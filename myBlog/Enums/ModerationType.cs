using System.ComponentModel;

namespace myBlog.Enums
{
    public enum ModerationType
    {
        [Description("Political propaganda")]
        Polictical,
        [Description("Offensive language")]
        Language,
        [Description("Drug references")]
        Drugs,
        [Description("Threatening speech")]
        Threatening,
        [Description("Sexual content")]
        Sexual,
        [Description("Hate Speech")]
        HateSpeech,
        [Description("Targeted shaming ")]
        Shaming
    }
}
