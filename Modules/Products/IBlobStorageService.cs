namespace clouddb_sdv_2022.Modules.Products
{
    internal interface IBlobStorageService
    {
        Task<string> UploadFileAsync(string containerName, string fileName, byte[] fileContent);
        Task DeleteFileAsync(string containerName, string fileName);
    }
}