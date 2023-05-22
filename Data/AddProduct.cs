using System.ComponentModel.DataAnnotations;
public class productitem{

    public string id {get; set;}

    [Required(ErrorMessage = "商品名を入力してください")]
    [StringLength(32, ErrorMessage = "商品名は32文字以下です。")] 
    public string Name { get; set; }

    [Required(ErrorMessage = "価格を入力してください")]
    [StringLength(9, ErrorMessage = "価格は9文字以下です。")] 
    public string Price { get; set; }

    [Required(ErrorMessage = "説明を入力してください")]
    [StringLength(2000, ErrorMessage = "説明文はは2000文字以下です。")] 
    public string Description {get; set; }

    [Required(ErrorMessage = "ジャンルを入力してください")]
    [StringLength(32, ErrorMessage = "ジャンルは32文字以下です。")] 
    public string? Genre {get; set; }

    [Url(ErrorMessage = "URLの形式を確認してください")] 
    public string? Url {get; set; }
    public DateTime? Date {get; set; }


}