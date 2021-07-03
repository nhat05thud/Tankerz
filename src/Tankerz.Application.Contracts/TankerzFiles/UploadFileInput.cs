namespace Tankerz.TankerzFiles
{
    public class UploadFileInput
    {
        public string Name { get; set; }
        public string Base64String { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public int FolderId { get; set; }
    }
}
