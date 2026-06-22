var container = new CustomServiceContainer();
container.RegisterSingleton<IEscapeRoomScript, EscapeRoomScript>();
container.RegisterTransient<IHintService, HintService>();
container.RegisterSingleton<IEscapeRoomUi, SpectreEscapeRoomUi>();
container.RegisterTransient<IEscapeRoomGame, EscapeRoomGame>();

await container.Resolve<IEscapeRoomGame>().RunAsync();

internal sealed class CustomServiceContainer
{
    private enum Lifetime
    {
        Singleton,
        Transient
    }

    private sealed record Registration(Type ServiceType, Type ImplementationType, Lifetime Lifetime);

    private readonly Dictionary<Type, Registration> registrations = new();
    private readonly Dictionary<Type, object> singletonInstances = new();

    public void RegisterSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        Register<TService, TImplementation>(Lifetime.Singleton);
    }

    public void RegisterTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        Register<TService, TImplementation>(Lifetime.Transient);
    }

    public TService Resolve<TService>()
        where TService : class
    {
        return (TService)Resolve(typeof(TService));
    }

    public object Resolve(Type serviceType)
    {
        if (registrations.TryGetValue(serviceType, out var registration))
        {
            return CreateInstance(registration);
        }

        if (serviceType.IsAbstract || serviceType.IsInterface)
        {
            throw new InvalidOperationException($"No registration found for {serviceType.FullName}.");
        }

        return CreateInstance(new Registration(serviceType, serviceType, Lifetime.Transient));
    }

    private void Register<TService, TImplementation>(Lifetime lifetime)
        where TService : class
        where TImplementation : class, TService
    {
        registrations[typeof(TService)] = new Registration(typeof(TService), typeof(TImplementation), lifetime);
    }

    private object CreateInstance(Registration registration)
    {
        if (registration.Lifetime == Lifetime.Singleton && singletonInstances.TryGetValue(registration.ServiceType, out var cached))
        {
            return cached;
        }

        var constructor = registration.ImplementationType
            .GetConstructors()
            .OrderByDescending(static c => c.GetParameters().Length)
            .FirstOrDefault();

        if (constructor is null)
        {
            throw new InvalidOperationException($"No public constructor found for {registration.ImplementationType.FullName}.");
        }

        var dependencies = constructor
            .GetParameters()
            .Select(parameter => Resolve(parameter.ParameterType))
            .ToArray();

        var instance = constructor.Invoke(dependencies);

        if (registration.Lifetime == Lifetime.Singleton)
        {
            singletonInstances[registration.ServiceType] = instance;
        }

        return instance;
    }
}
