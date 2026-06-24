# Tips to Reduce Token Usage When Using AI for Coding

Token usage directly affects cost and response quality. The more precise and lean your prompts, the better—and cheaper—your AI interactions become. All examples below use C# / .NET.

---

## 1. Be Specific, Not Verbose

Avoid long preambles and get straight to the point.

❌ **Before (~45 tokens):**
```
Hi! I hope you can help. I'm working on a .NET 8 Web API project and I have a controller
and I was wondering if there's something wrong with how I'm returning responses, could you
take a look and let me know what you think?
```

✅ **After (~12 tokens):**
```
Fix incorrect HTTP response codes in this ASP.NET Core controller action.
```

| | Tokens |
|---|---|
| Before | ~45 |
| After | ~12 |
| **Saved** | **~33 (~73%)** |

---

## 2. Paste Only Relevant Code

Don't dump entire files. Trim to the method or class that's relevant.

❌ **Before (~120 tokens):**
```csharp
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Services;
using MyApp.Models;
using MyApp.Repositories;
using MyApp.Helpers;
using MyApp.DTOs;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        // The problem is here
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }
    }
}
```

✅ **After (~30 tokens):**
```csharp
public async Task<IActionResult> GetOrder(int id)
{
    var order = await _orderService.GetByIdAsync(id);
    return Ok(order);
}
// Issue: no null check — returns 200 with null body when order not found
```

| | Tokens |
|---|---|
| Before | ~120 |
| After | ~30 |
| **Saved** | **~90 (~75%)** |

---

## 3. Use File References Instead of Full File Contents

When using Copilot in Visual Studio or VS Code, use `#file:` references instead of copying file contents into the chat.

❌ **Before (~300+ tokens):**
```
Here is my full Program.cs: [pastes 200 lines of startup config]
Why isn't my JWT middleware working?
```

✅ **After (~15 tokens):**
```
#file:Program.cs
Why isn't my JWT middleware applying to the /orders route?
```

| | Tokens |
|---|---|
| Before | ~300+ |
| After | ~15 (reference handled by tool) |
| **Saved** | **~285+ (~95%)** |

---

## 4. Avoid Repeating Context

In a multi-turn conversation, don't re-explain what was already said.

❌ **Before (~60 tokens):**
```
Earlier you helped me fix the GetOrder method in my ASP.NET Core controller that was
returning 200 with a null body. Now I have the same issue in another method called
DeleteOrder — it also doesn't handle the case where the order doesn't exist. Can you fix that too?
```

✅ **After (~12 tokens):**
```
Apply the same null-check fix to DeleteOrder.
```

| | Tokens |
|---|---|
| Before | ~60 |
| After | ~12 |
| **Saved** | **~48 (~80%)** |

---

## 5. Prefer Short, Focused Prompts

Break complex tasks into smaller steps rather than asking for everything at once.

❌ **Before (~35 tokens, ~400 token response):**
```
Add input validation, logging, error handling, and a unit test to this service method.
```

✅ **After (split into steps, ~10 tokens each, ~100 token responses):**
```
Step 1: Add FluentValidation to this C# service method. Return only the method.
```
```
Step 2: Add ILogger<T> error logging to the method you just updated.
```

| | Tokens (combined) |
|---|---|
| Before (one response) | ~400+ |
| After (two focused responses) | ~200 |
| **Saved** | **~200 (~50%)** |

---

## 6. Specify the Output Format Upfront

Tell the AI exactly what you want — it prevents long explanations you don't need.

❌ **Before (~8 token prompt, ~180 token response with explanation):**
```
How do I add caching to this method?
```

✅ **After (~18 token prompt, ~60 token response — code only):**
```
Add IMemoryCache to this C# method. Return only the updated method, no explanation.
```

```csharp
// Returned — just the updated method:
public Product GetProduct(int id)
{
    return _cache.GetOrCreate($"product_{id}", entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return _repo.FindById(id);
    });
}
```

| | Tokens |
|---|---|
| Before (response) | ~180 |
| After (response) | ~60 |
| **Saved** | **~120 (~67%)** |

---

## 7. Use Structured Prompts

Structured prompts reduce ambiguity and prevent follow-up clarification questions.

❌ **Before (~25 tokens, vague — ~350 token response):**
```
Can you make a service for handling orders in my .NET app?
```

✅ **After (~35 tokens, structured — ~140 token response):**
```
Language: C# / .NET 8
Task: Create an OrderService class
Interface: IOrderService
Methods: GetByIdAsync(int id), CreateAsync(OrderDto dto)
Dependencies: inject IOrderRepository
Return: Only the class and interface, no explanation
```

| | Tokens |
|---|---|
| Before (response) | ~350 |
| After (response) | ~140 |
| **Saved** | **~210 (~60%)** |

---

## 8. Avoid Asking for What You Can Look Up

Don't use AI for syntax you can find in the Microsoft docs. Use it for reasoning and debugging.

❌ **Before (~20 tokens, ~80 token generic response):**
```
What's the syntax for a LINQ GroupBy with a select in C#?
```

✅ **After — use AI for reasoning, not lookup (~25 tokens, ~40 token targeted response):**
```
My LINQ GroupBy is returning duplicate groups on OrderDate — here's the query. What's wrong?

var grouped = orders.GroupBy(o => o.OrderDate).Select(g => g.First());
```

| | Tokens |
|---|---|
| Before (response) | ~80 (generic syntax explanation) |
| After (response) | ~40 (targeted fix) |
| **Saved** | **~40 (~50%)** |

---

## 9. Summarize Long Errors

Paste only the key exception line and location, not the full stack trace.

❌ **Before (~200 tokens):**
```
System.NullReferenceException: Object reference not set to an instance of an object.
   at MyApp.Services.OrderService.ProcessAsync(Order order) in /src/Services/OrderService.cs:line 42
   at MyApp.Controllers.OrdersController.Post(OrderDto dto) in /src/Controllers/OrdersController.cs:line 18
   at lambda_method123(Closure, Object, Object[])
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor...
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker...
   [30 more lines...]
```

✅ **After (~25 tokens):**
```
NullReferenceException at OrderService.ProcessAsync line 42 in OrderService.cs.
order.Customer is null despite being required. How do I guard against this?
```

| | Tokens |
|---|---|
| Before | ~200 |
| After | ~25 |
| **Saved** | **~175 (~88%)** |

---

## 10. Use System Prompts / Instructions Efficiently

Define your stack once in a system/custom instruction so you never repeat it per message.

❌ **Before — repeated in every message (~40 token overhead per prompt):**
```
I'm using C# .NET 8, ASP.NET Core Web API, Entity Framework Core with SQL Server,
and I follow Clean Architecture. Please help me with...
```

✅ **After — set once as a custom instruction, then just ask:**
```
[System instruction]: Stack: C# .NET 8, ASP.NET Core Web API, EF Core, SQL Server, Clean Architecture.
```
```
Add soft-delete support to the Order entity.
```

| | Tokens (per prompt) |
|---|---|
| Before | ~40 overhead every time |
| After | ~0 overhead (defined once) |
| **Saved** | **~40 per prompt** |

---

## 11. Avoid Asking for Alternatives Unless Needed

Ask for the best approach first. Only request alternatives if it doesn't fit.

❌ **Before (~15 tokens, ~300 token response):**
```
Give me 3 ways to implement background jobs in .NET 8.
```

✅ **After (~12 tokens, ~80 token response):**
```
Best way to run a background job in .NET 8 with no extra packages?
```

```csharp
public class MyJob : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await DoWorkAsync();
            await Task.Delay(TimeSpan.FromMinutes(1), ct);
        }
    }
}
```

| | Tokens |
|---|---|
| Before (response) | ~300 |
| After (response) | ~80 |
| **Saved** | **~220 (~73%)** |

---

## 12. Reuse Context with Session Continuity

Keep the session open and build on it instead of starting fresh each time.

❌ **Before — new session, re-establishing context (~80 tokens):**
```
I have a C# .NET 8 ASP.NET Core Web API using Clean Architecture with EF Core and SQL Server.
I was working on an OrderService earlier. I need to add a method to cancel an order.
```

✅ **After — continuing same session (~12 tokens):**
```
Add a CancelOrderAsync(int id) method to OrderService.
```

| | Tokens |
|---|---|
| Before | ~80 |
| After | ~12 |
| **Saved** | **~68 (~85%)** |

---

## 13. Strip Comments and Docstrings from Pasted Code

Unless the AI needs the comments to understand intent, remove XML docs and inline comments before pasting.

❌ **Before (~95 tokens with XML docs and inline comments):**
```csharp
/// <summary>
/// Calculates the total price for an order including tax.
/// </summary>
/// <param name="order">The order to calculate.</param>
/// <returns>The total price as a decimal.</returns>
public decimal CalculateTotal(Order order)
{
    // Get the subtotal by summing line items
    var subtotal = order.Items.Sum(i => i.Price * i.Quantity);
    // Apply the tax rate from config
    var tax = subtotal * _config.TaxRate;
    // Return the final total
    return subtotal + tax;
}
```

✅ **After (~30 tokens):**
```csharp
public decimal CalculateTotal(Order order)
{
    var subtotal = order.Items.Sum(i => i.Price * i.Quantity);
    return subtotal + (subtotal * _config.TaxRate);
}
```

| | Tokens |
|---|---|
| Before | ~95 |
| After | ~30 |
| **Saved** | **~65 (~68%)** |

---

## 14. Ask for Diffs, Not Full Rewrites

Request only the changed lines to avoid receiving the entire unchanged file in the response.

❌ **Before (~12 token prompt, ~250 token response):**
```
Add async/await to the OrderService class. Return the full updated class.
```

✅ **After (~15 token prompt, ~60 token response):**
```
Add async/await to OrderService. Show only the method signatures that change, no explanation.
```

```csharp
// Only the changed signatures:
public async Task<Order?> GetByIdAsync(int id) { ... }
public async Task<Order> CreateAsync(OrderDto dto) { ... }
public async Task DeleteAsync(int id) { ... }
```

| | Tokens |
|---|---|
| Before (response) | ~250 |
| After (response) | ~60 |
| **Saved** | **~190 (~76%)** |

---

## Real-World End-to-End Example

A single ASP.NET Core endpoint debug session — before and after applying all tips.

### ❌ Before (~410 tokens total)

**Prompt (~180 tokens):**
```
Hi! I'm building a .NET 8 Web API. I have a controller that retrieves orders and I'm not
sure if the error handling is right. I added XML comments too. Could you review it and suggest
improvements? Here is the whole file:

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Services;
using MyApp.Models;
using MyApp.Repositories;
using MyApp.Helpers;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        /// <summary>Constructor</summary>
        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        /// <summary>Gets an order by ID</summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            // Try to get the order
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                return Ok(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "error");
            }
        }
    }
}
```

**Response (~230 tokens):**
*(Long explanation covering unused usings, Console.WriteLine vs ILogger, missing 404 handling,
generic 500 message, XML doc style — ends with "Want me to rewrite the whole class?")*

---

### ✅ After (~115 tokens total)

**Prompt (~45 tokens):**
```
Fix error handling in this ASP.NET Core action. Return only the updated method, no explanation.

[HttpGet("{id}")]
public async Task<IActionResult> GetOrder(int id)
{
    try
    {
        var order = await _orderService.GetByIdAsync(id);
        return Ok(order);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
        return StatusCode(500, "error");
    }
}
```

**Response (~70 tokens):**
```csharp
[HttpGet("{id}")]
public async Task<IActionResult> GetOrder(int id)
{
    var order = await _orderService.GetByIdAsync(id);
    if (order is null) return NotFound();
    return Ok(order);
}
```
*(Exception handling delegated to global middleware — assumed already configured)*

---

### Token Comparison

| Area | Before | After | Saved |
|------|--------|-------|-------|
| Prompt tokens | ~180 | ~45 | **~135** |
| Response tokens | ~230 | ~70 | **~160** |
| **Total** | **~410** | **~115** | **~295 (~72%)** |

---

## Summary Table

| Tip | .NET Context | Token Saving Area |
|-----|-------------|-------------------|
| Be specific | Skip preamble, state the task | Input |
| Paste only relevant code | One method, not the whole controller | Input |
| Use file references | `#file:OrderService.cs` in Copilot | Input |
| Avoid repeating context | Build on previous turns | Input |
| Short focused prompts | One concern per prompt | Input + Output |
| Specify output format | "Return only the method" | Output |
| Use structured prompts | List language, task, constraints | Input + Output |
| Avoid lookup questions | Use docs for syntax, AI for logic | Output |
| Summarize long errors | One line: exception + location | Input |
| Use system instructions | Define stack once | Input |
| Avoid asking for alternatives | Ask for the best approach first | Output |
| Reuse session continuity | Don't restart sessions | Input |
| Strip comments/docstrings | Remove XML docs before pasting | Input |
| Ask for diffs, not rewrites | "Show only changed signatures" | Output |
