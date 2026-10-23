namespace ImageFiltering.Core;

public class FilterPipeline : FilterBase
{
    private readonly List<IFilter> _filters = [];

    public FilterPipeline(params IFilter[] filters)
    {
        if (filters != null)
        {
            foreach (var filter in filters)
            {
                Add(filter);
            }
        }
    }
    
    public override Image Apply(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);

        foreach (var filter in _filters)
        {
            image = filter.Apply(image);
        }
        return image;
    }

    public void Add(IFilter other)
    {
        ArgumentNullException.ThrowIfNull(other);

        if (ReferenceEquals(other, this))
        {
            throw new ArgumentException("A pipeline cannot be added to itself.", nameof(other));
        }

        if (other is FilterPipeline pipeline)
        {
            foreach (var filter in pipeline._filters)
            {
                _filters.Add(filter);
            }
        }
        else
        {
            _filters.Add(other);
        }
    }
}