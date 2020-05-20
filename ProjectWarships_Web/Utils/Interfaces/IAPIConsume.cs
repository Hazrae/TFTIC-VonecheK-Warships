namespace ProjectWarships_Web.Utils
{
    public interface IAPIConsume
    {
        void Delete(string action, int id);
        T Get<T>(string action, int? id = null);
        void Post<T>(string action, T item);
        T2 PostWithReturn<T,T2>(string action, T item);
        void Put<T>(string action, T item);
        T2 PutWithReturn<T, T2>(string action, T item);
    }
}