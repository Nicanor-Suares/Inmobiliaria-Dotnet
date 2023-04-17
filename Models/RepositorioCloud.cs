using MySql.Data.MySqlClient;
// Azure Libraries
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using System.Threading.Tasks;

namespace Inmobiliaria_DotNet.Models;

public class RepositorioCloud {

    private readonly BlobServiceClient blobServiceClient;
    private readonly string containerName;

    public RepositorioCloud(string connectionString, string containerName)
    {
        blobServiceClient = new BlobServiceClient(connectionString);
        this.containerName = containerName;
    }

    public async Task<string> SubirAvatarAsync(string fileName, IFormFile file)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        //Si ya existe, la borra
        await blobClient.DeleteIfExistsAsync();

        var fileExtension = Path.GetExtension(file.FileName);
        blobClient = containerClient.GetBlobClient(fileName + fileExtension);
        await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
        });

        return blobClient.Uri.ToString();
    }
    public async Task BorrarAvatar(string fileName)
    {
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.DeleteAsync();
    }


}