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
	private readonly string connectionString;

	public RepositorioCloud(string connectionString, string containerName)
	{
		blobServiceClient = new BlobServiceClient(connectionString);
		this.containerName = containerName;
		this.connectionString = connectionString;
	}

	public async Task<string> SubirAvatarAsync(string fileName, IFormFile file)
	{
		var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
		var blobClient = containerClient.GetBlobClient(fileName);
		//Si ya existe, la borra
		await blobClient.DeleteIfExistsAsync();

		blobClient = containerClient.GetBlobClient(fileName);
		await blobClient.UploadAsync(file.OpenReadStream(), new BlobUploadOptions
		{
			HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType }
		});

		return blobClient.Uri.ToString();
	}
	public async Task BorrarAvatar(string fileName)
	{
		try
		{
			var blobUri = new Uri(fileName);
			var blobName = Path.GetFileName(blobUri.LocalPath);

			var containerClient = new BlobContainerClient(connectionString, containerName);
			var blobClient = containerClient.GetBlobClient(blobName);

			await blobClient.DeleteAsync();
		}
		catch (System.Exception)
		{
			throw;
		}
	}


}