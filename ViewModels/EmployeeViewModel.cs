using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace UCOMProject.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "工號")]
        public string EId { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "中文姓名")]
        public string Name { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "英文名")]
        //[RegularExpression("/^[A-Za-z][A-Za-z0-9]*$/", ErrorMessage = "請輸入英文字母")]
        public string EnglishName { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "身分證字號")]
        public string Password { get; set; }

        [Required(ErrorMessage ="*")]
        [Display(Name = "員工大頭照")]
        public HttpPostedFileBase ImageFile { get; set; }
        [BindNever]
        public string Image { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "班別")]
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        public ShiftType ShiftType { get; set; }

        [BindNever]
        public string Shift { get { return ShiftType.ToString(); } set { } }

        [Required(ErrorMessage = "*")]
        [Display(Name = "單位")]
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        public BranchType BranchType { get; set; }
        [BindNever]
        public string Branch { get { return BranchType.ToString(); } set { } }

        [Required(ErrorMessage = "*")]
        [Display(Name = "職位")]
        [Range(1, int.MaxValue, ErrorMessage = "*")]
        public JobTitleType JobType { get; set; }

        [BindNever]
        public int JobRank { get { return (int)JobType; }set { } }
        [BindNever]
        public string JobTitle { get { return JobType.ToString(); } set { } }

        [Required()]
        [Display(Name = "性別")]
        public bool SexType { get; set; }

        [BindNever]
        public string Sex { get { return SexType ? "男" : "女"; } set { } }

        [Display(Name = "信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效電子郵件")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Phone(ErrorMessage = "請輸入手機號碼")]
        [Display(Name = "手機號碼")]
        public string Phone { get; set; }


        [Required(ErrorMessage = "*")]
        [Display(Name = "到職日")]
        public DateTime StartDate { get; set; }

        [BindNever]
        public bool Allow { get; set; }
    }
}