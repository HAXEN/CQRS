using System.Threading.Tasks;

namespace CQRS.Commanding.Testing
{
    public static class TaskExtensions
    {
        public static Task<T> ToTaskResult<T>(this T obj) => Task.FromResult(obj);
    }
}
