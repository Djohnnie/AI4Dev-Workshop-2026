namespace EditDistanceWorkshop.Solution;

public static class EditDistanceSampleData
{
    public static EditDistanceCase CreateWorkshopCase()
    {
        return new("kitten", "sitting");
    }

    public static EditDistanceCase CreateBenchmarkCase()
    {
        return new("distance", "instance");
    }
}
