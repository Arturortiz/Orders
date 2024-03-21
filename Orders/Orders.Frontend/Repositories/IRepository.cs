namespace Orders.Frontend.Repositories
{
    public interface IRepository
    {
        //video 4 min 20
        Task<HttpResponseWrapper<T>> GetAsync<T>(String url);

        Task<HttpResponseWrapper<Object>> PostAsync<T>(String url, T model);

        Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(String url, T model);

    }
}
