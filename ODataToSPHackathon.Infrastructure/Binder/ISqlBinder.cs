namespace ODataToSPHackathon.Infrastructure
{
    public interface ISqlBinder
    {
        IQueryResolver Query { get; set; }
        string Resolve();
    }
}