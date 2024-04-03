namespace Orders.Frontend.Repositories
{
    public interface IRepository
    {
        //video 4 min 20
        //video 7 min 9
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model);
        Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model);
        Task<HttpResponseWrapper<object>> DeleteAsync<T>(string url);
        Task<HttpResponseWrapper<object>> PutAsync<T>(string url, T model);
        Task<HttpResponseWrapper<TActionResponse>> PutAsync<T, TActionResponse>(string url, T model);
    }
}
