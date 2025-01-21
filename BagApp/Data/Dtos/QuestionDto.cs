namespace BagApp.Data.Dtos
{
    public class QuestionDto : IDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleEn { get; set; }
        public string DescriptionEn { get; set; }
        public string TitleAr { get; set; }
        public string DescriptionAr { get; set; }
    }
}
