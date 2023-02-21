using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace UCOMProject.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "工號")]
        public string EId { get; set; }

        [Required]
        [Display(Name = "中文姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "身分證字號")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "員工大頭照")]
        public HttpPostedFileBase Image { get; set; }

        [Required]
        [Display(Name = "單位")]
        public BranchType Branch { get; set; }

        [Required]
        [Display(Name = "職位")]
        public JobTitleType JobTitle { get; set; }
        public int JobRank { get { return (int)JobTitle; } }


        [Required]
        [Display(Name = "性別")]
        public bool Sex { get; set; }
        public string SexType { get { return Sex ? "男" : "女"; } }

        [Display(Name = "信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效電子郵件")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "請輸入手機號碼")]
        [Display(Name = "手機號碼")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "英文名字")]
        //[RegularExpression("/^[A-Za-z][A-Za-z0-9]*$/", ErrorMessage = "請輸入英文字母")]
        public string EnglishName { get; set; }

        [Required]
        [Display(Name = "班別")]
        public ShiftType Shift { get; set; }

        [Required]
        [Display(Name = "到職日")]
        public DateTime StartDate { get; set; }

        public bool Allow { get; set; }
    }
}