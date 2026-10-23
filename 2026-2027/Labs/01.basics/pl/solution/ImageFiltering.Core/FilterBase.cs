namespace ImageFiltering.Core;

public abstract class FilterBase : IFilter
{
    public abstract Image Apply(Image image);

    public static FilterPipeline operator +(FilterBase first, FilterBase second)
    {
        ArgumentNullException.ThrowIfNull(first);
        ArgumentNullException.ThrowIfNull(second);

        FilterPipeline pipeline = new FilterPipeline();
        pipeline.Add(first);
        pipeline.Add(second);
        return pipeline;
    }
}