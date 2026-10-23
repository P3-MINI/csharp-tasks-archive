namespace ImageFiltering.Core;

public interface IFilter
{
    Image Apply(Image image);
}