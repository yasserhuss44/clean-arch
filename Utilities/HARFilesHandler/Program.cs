using HARCore;
using HttpArchive;

namespace HARFilesHandler;

internal class Program
{
    static void Main(string[] args)
    {
        MergeHARFiles(@"C:\Users\lenovo\Desktop\NDP API Gateway");
    } 
    
    
    static void MergeHARFiles(string  filepath)
    {
      var  _harObj = new Har(); 
        string pageRef = Guid.NewGuid().ToString();
        _harObj.Log.Pages.Add(new Page() { Id = pageRef, StartedDateTime = DateTime.UtcNow, Title = "Log Page" });

        var harFiles =  System.IO.Directory.GetFiles(filepath, "*.har");
        foreach (var item in harFiles)
        {
            try
            {

            var newHar =  Har.Deserialize(System.IO.File.ReadAllText(item));
            foreach (var entry in newHar.Log.Entries)
            {
                   
                    entry.PageRef = pageRef;
                 _harObj.Log.Entries.Add(entry);
            }

            }
            catch (Exception)
            {

              
            }
        }
        var harJson = Har.Serialize(_harObj);

        System.IO.File.WriteAllText(filepath + "\\" + DateTime.Now.ToString("ddHHmmss") + ".har", harJson, System.Text.Encoding.UTF8);

    }
}
