namespace Inmobiliaria_DotNet.Models;

public class Database {
  
  public int id {get; set;}
  public string container {get; set;}
  public string access_key {get; set;}
  public string connection_string {get; set;}
  
  public Database(){
    this.id = 0;
    this.container = "";
    this.access_key = "";
    this.connection_string = "";
  }
  public Database(string container, string access_key, string connection_string) {
    this.id = 0;
    this.container = container;
    this.access_key = access_key;
    this.connection_string = connection_string;
  }

}