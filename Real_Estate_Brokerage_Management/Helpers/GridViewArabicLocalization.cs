using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls.UI.Localization;

namespace Helper.Helpers
{
    public class MyArabicRadGridLocalizationProvider : RadGridLocalizationProvider
    {
        //public override CultureInfo Culture
        //{
        //    get
        //    {
        //        return new CultureInfo("ar-EG");
        //    }
        //}

        public override string GetLocalizedString(string id)
        {

            switch (id)
            {
                case RadGridStringId.FilterOperatorBetween: return "بينهما";
                case RadGridStringId.FilterOperatorContains: return "يحتوي على";
                case RadGridStringId.FilterOperatorDoesNotContain: return "لا يحتوي على";
                case RadGridStringId.FilterOperatorEndsWith: return "ينتهي بـ";
                case RadGridStringId.FilterOperatorEqualTo: return "يساوي";
                case RadGridStringId.FilterOperatorGreaterThan: return "أكثر من";
                case RadGridStringId.FilterOperatorGreaterThanOrEqualTo: return "أكبر من أو يساوي";
                case RadGridStringId.FilterOperatorIsEmpty: return "فارغ";
                case RadGridStringId.FilterOperatorIsNull: return "فارغ";
                case RadGridStringId.FilterOperatorLessThan: return "اقل من";
                case RadGridStringId.FilterOperatorLessThanOrEqualTo: return "اصغر من او يساوي";
                case RadGridStringId.FilterOperatorNoFilter: return "لا يوجد تصفية";
                case RadGridStringId.FilterOperatorNotBetween: return "ليس بينهما";
                case RadGridStringId.FilterOperatorNotEqualTo: return "غير متساوي";
                case RadGridStringId.FilterOperatorNotIsEmpty: return "ليست فارغة";
                case RadGridStringId.FilterOperatorNotIsNull: return "ليست فارغة";
                case RadGridStringId.FilterOperatorStartsWith: return "يبدأ بـ";
                case RadGridStringId.FilterOperatorIsLike: return "كيف";
                case RadGridStringId.FilterOperatorNotIsLike: return "كيف لا";
                case RadGridStringId.FilterOperatorIsContainedIn: return "متضمن في";
                case RadGridStringId.FilterOperatorNotIsContainedIn: return "ليس جزءا";
                case RadGridStringId.FilterOperatorCustom: return "وظيفة مصممة";
                case RadGridStringId.FilterFunctionBetween: return "بينهما";
                case RadGridStringId.FilterFunctionContains: return "يحتوي على";
                case RadGridStringId.FilterFunctionDoesNotContain: return "لا يحتوي على";
                case RadGridStringId.FilterFunctionEndsWith: return "ينتهي بـ";
                case RadGridStringId.FilterFunctionEqualTo: return "متساوي";
                case RadGridStringId.FilterFunctionGreaterThan: return "أكثر من";
                case RadGridStringId.FilterFunctionGreaterThanOrEqualTo: return "أكبر من أو يساوي";
                case RadGridStringId.FilterFunctionIsEmpty: return "فارغ";
                case RadGridStringId.FilterFunctionIsNull: return "فارغ";
                case RadGridStringId.FilterFunctionLessThan: return "اقل من";
                case RadGridStringId.FilterFunctionLessThanOrEqualTo: return "اصغر من او يساوي";
                case RadGridStringId.FilterFunctionNoFilter: return "لا يوجد تصفية";
                case RadGridStringId.FilterFunctionNotBetween: return "ليس بينهما";
                case RadGridStringId.FilterFunctionNotEqualTo: return "غير متساوي";
                case RadGridStringId.FilterFunctionNotIsEmpty: return "ليست فارغة";
                case RadGridStringId.FilterFunctionNotIsNull: return "ليس فارغ";
                case RadGridStringId.FilterFunctionStartsWith: return "يبدأ بـ";
                case RadGridStringId.FilterFunctionCustom: return "وظيفة مصممة";
                case RadGridStringId.FilterMenuAvailableFilters: return "الفلاتر المتوفرة";
                case RadGridStringId.FilterMenuClearFilters: return "مسح عوامل التصفية";
                case RadGridStringId.FilterMenuButtonOK: return "موافق";
                case RadGridStringId.FilterMenuButtonCancel: return "إلغاء";
                case RadGridStringId.FilterMenuSearchBoxText: return "...بحث";
                case RadGridStringId.FilterMenuSelectionAll: return "اختيار الكل";
                case RadGridStringId.FilterMenuSelectionNull: return "التصفية فارغ";
                case RadGridStringId.FilterMenuSelectionNotNull: return "التصفية ليس فارغًا";
                case RadGridStringId.FilterMenuBlanks: return "تصفية الفراغات القائمة";
                case RadGridStringId.FilterMenuSelectionAllSearched: return "اختيار الكل";
                case RadGridStringId.FilterLogicalOperatorOr: return "اختيار الكل";
                case RadGridStringId.FilterLogicalOperatorAnd: return "تصفية المشغل المنطقي و";
                case RadGridStringId.FilterFunctionYesterday: return "وظيفة التصفية أمس";
                case RadGridStringId.FilterFunctionToday: return "وظيفة التصفية اليوم";
                case RadGridStringId.FilterFunctionSelectedDates: return "وظيفة التصفية التواريخ المحددة";
                case RadGridStringId.FilterFunctionDuringLast7days: return "وظيفة التصفية خلال آخر 7 أيام";
                case RadGridStringId.FilterCompositeNotOperator: return "عامل التصفية ليس مركبًا";

                case RadGridStringId.CustomFilterMenuItem: return "عنصر قائمة التصفية المخصص";
                case RadGridStringId.CustomFilterDialogCaption: return "مربع حوار مرشح مخصص";
                case RadGridStringId.CustomFilterDialogLabel: return "إظهار الخطوط التي:";
                case RadGridStringId.CustomFilterDialogRbAnd: return "و";
                case RadGridStringId.CustomFilterDialogRbOr: return "أو";
                case RadGridStringId.CustomFilterDialogBtnOk: return "موافق";
                case RadGridStringId.CustomFilterDialogBtnCancel: return "إلغاء";
                case RadGridStringId.CustomFilterDialogTrue: return "مربع حوار التصفية المخصصة صحيح";
                case RadGridStringId.CustomFilterDialogFalse: return "مربع حوار عامل التصفية المخصص خطأ";
                case RadGridStringId.CustomFilterDialogCheckBoxNot: return "لا";

                case RadGridStringId.ColumnChooserFilterTextBoxNullText: return "مربع النص لتصفية منتقي الأعمدة نص فارغ";
                case RadGridStringId.CompositeFilterFormErrorCaption: return "تسمية توضيحية لخطأ نموذج عامل التصفية المركب";
                case RadGridStringId.CompositeFilterFormInvalidFilter: return "نموذج عامل التصفية المركب عامل تصفية غير صالح";

                case RadGridStringId.DeleteRowMenuItem: return "حذف سطر";
                case RadGridStringId.SortAscendingMenuItem: return "فرز بترتيب تصاعدي";
                case RadGridStringId.SortDescendingMenuItem: return "فرز بترتيب تنازلي";
                case RadGridStringId.ClearSortingMenuItem: return "إلغاء الفرز";
                case RadGridStringId.GroupByThisColumnMenuItem: return "تتناسب مع هذا العمود";
                case RadGridStringId.UngroupThisColumn: return "احذف هذا العمود من المجموعة";
                case RadGridStringId.ColumnChooserMenuItem: return "محدد العمود";
                case RadGridStringId.HideMenuItem: return "إخفاء";
                case RadGridStringId.UnpinMenuItem: return "إلغاء التثبيت";
                case RadGridStringId.UnpinRowMenuItem: return "إلغاء التثبيت";
                case RadGridStringId.PinAtTopMenuItem: return "تثبيت العمود في الأعلى";
                case RadGridStringId.PinAtBottomMenuItem: return "تثبيت العمود في الأسفل";
                case RadGridStringId.PinMenuItem: return "تثبيت العمود";
                case RadGridStringId.PinAtLeftMenuItem: return "تثبيت العمود على اليسار";
                case RadGridStringId.PinAtRightMenuItem: return "تثبيت العمود على اليمين";
                case RadGridStringId.BestFitMenuItem: return "أنسب حجم للعمود";
                case RadGridStringId.PasteMenuItem: return "لصق";
                case RadGridStringId.EditMenuItem: return "تحرير";
                case RadGridStringId.CopyMenuItem: return "نسخ";
                case RadGridStringId.CutMenuItem: return "قطع";
                case RadGridStringId.ClearValueMenuItem: return "حذف القيمة";

                case RadGridStringId.AddNewRowString: return "انقر هنا لإدراج سطر جديد";

                case RadGridStringId.ConditionalFormattingMenuItem: return "التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingCaption: return "محرر التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingLblColumn: return "التنسيق الشرطي وفق";
                case RadGridStringId.ConditionalFormattingLblName: return "تحديد قاعدة التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingLblType: return "نوع التنسيق";
                case RadGridStringId.ConditionalFormattingRuleAppliesOn: return "تنطبق على";
                case RadGridStringId.ConditionalFormattingLblValue1: return "القيمة ١";
                case RadGridStringId.ConditionalFormattingLblValue2: return "القيمة ٢";
                case RadGridStringId.ConditionalFormattingGrpConditions: return "نماذج التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingGrpProperties: return "صفات";
                case RadGridStringId.ConditionalFormattingChkApplyToRow: return "تنطبق على الخط";
                case RadGridStringId.ConditionalFormattingBtnAdd: return "إضافة";
                case RadGridStringId.ConditionalFormattingBtnRemove: return "حذف";
                case RadGridStringId.ConditionalFormattingBtnOK: return "نعم";
                case RadGridStringId.ConditionalFormattingBtnCancel: return "إلغاء";
                case RadGridStringId.ConditionalFormattingBtnApply: return "تطبيق";
                case RadGridStringId.ConditionalFormattingLblFormat: return "خصائص التنسيق";
                case RadGridStringId.ConditionalFormattingChkApplyOnSelectedRows: return "يتم تطبيق التنسيق الشرطي Chk على الصفوف المحددة";
                case RadGridStringId.ConditionalFormattingSortAlphabetically: return "فرز أبجديا";
                case RadGridStringId.ConditionalFormattingBtnExpression: return "التنسيق الشرطي تعبير Btn";
                case RadGridStringId.ConditionalFormattingTextBoxExpression: return "اكتب الشرط";
                case RadGridStringId.ConditionalFormattingChooseOne: return "اختر الشرط";
                case RadGridStringId.ConditionalFormattingCondition: return "حالة التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingContains: return "يحتوي على";
                case RadGridStringId.ConditionalFormattingDoesNotContain: return "لا يحتوي على";
                case RadGridStringId.ConditionalFormattingEndsWith: return "ينتهي بـ";
                case RadGridStringId.ConditionalFormattingEqualsTo: return "متساوي";
                case RadGridStringId.ConditionalFormattingExpression: return "تعبير التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingInvalidParameters: return "معلمات غير صالحة";
                case RadGridStringId.ConditionalFormattingIsBetween: return "بينهما";
                case RadGridStringId.ConditionalFormattingIsGreaterThan: return "أكبر من";
                case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual: return "أكبر من أو يساوي";
                case RadGridStringId.ConditionalFormattingIsLessThan: return "أقل من";
                case RadGridStringId.ConditionalFormattingIsLessThanOrEqual: return "أقل من أو يساوي";
                case RadGridStringId.ConditionalFormattingIsNotBetween: return "ليس بينهما";
                case RadGridStringId.ConditionalFormattingIsNotEqualTo: return "غير متساوي";
                case RadGridStringId.ConditionalFormattingItem: return "عنصر التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingPleaseSelectValidCellValue: return "التنسيق الشرطي الرجاء تحديد قيمة خلية صالحة";
                case RadGridStringId.ConditionalFormattingPleaseSetValidCellValue: return "التنسيق الشرطي يرجى تعيين قيمة خلية صالحة";
                case RadGridStringId.ConditionalFormattingPleaseSetValidCellValues: return "التنسيق الشرطي يرجى تعيين قيم صالحة للخلايا";
                case RadGridStringId.ConditionalFormattingPleaseSetValidExpression: return "التنسيق الشرطي يرجى تعيين تعبير صالح";
                case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitive: return "حساسة لحالة الأحرف";
                case RadGridStringId.ConditionalFormattingPropertyGridCaseSensitiveDescription: return "وصف حساس لحالة حالة خاصية التنسيق الشرطي للشبكة";
                case RadGridStringId.ConditionalFormattingPropertyGridCellBackColor: return "لون خلفية الخلية";
                case RadGridStringId.ConditionalFormattingPropertyGridCellBackColorDescription: return "اختر لون خلفية الخلية";
                case RadGridStringId.ConditionalFormattingPropertyGridCellFont: return "خط الخلية";
                case RadGridStringId.ConditionalFormattingPropertyGridCellFontDescription: return "اختر خط الخلية لتطبيقة علي البيانات المختارة";
                case RadGridStringId.ConditionalFormattingPropertyGridCellForeColor: return "لون خط الخلية";
                case RadGridStringId.ConditionalFormattingPropertyGridCellForeColorDescription: return "خلية الشبكة وصف اللون الأمامي";
                case RadGridStringId.ConditionalFormattingStartsWith: return "يبدأ بـ";
                case RadGridStringId.ConditionalFormattingPropertyGridTextAlignmentDescription: return "وصف محاذاة نص الشبكة الخاصة بالتنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingPropertyGridTextAlignment: return "محاذاة نص الشبكة";
                case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignmentDescription: return "خاصية التنسيق الشرطي وصف محاذاة النص لصف الشبكة";
                case RadGridStringId.ConditionalFormattingPropertyGridRowTextAlignment: return "محاذاة نص الصف";
                case RadGridStringId.ConditionalFormattingPropertyGridRowForeColorDescription: return "صف الشبكة وصف اللون الأمامي";
                case RadGridStringId.ConditionalFormattingPropertyGridRowForeColor: return "لون خط الصف";
                case RadGridStringId.ConditionalFormattingPropertyGridRowFontDescription: return "وصف خط خاصية التنسيق الشرطي لصف الشبكة";
                case RadGridStringId.ConditionalFormattingPropertyGridRowFont: return "خط الصف";
                case RadGridStringId.ConditionalFormattingPropertyGridRowBackColorDescription: return "خاصية التنسيق الشرطي وصف اللون الخلفي لصف الشبكة";
                case RadGridStringId.ConditionalFormattingPropertyGridRowBackColor: return "لون خلفية الصف";
                case RadGridStringId.ConditionalFormattingPropertyGridEnabledDescription: return "وصف شبكة خاصية التنسيق الشرطي";
                case RadGridStringId.ConditionalFormattingPropertyGridEnabled: return "تم تمكين شبكة خصائص التنسيق الشرطي";

                case RadGridStringId.ColumnChooserFormCaption: return "محدد العمود";
                case RadGridStringId.ColumnChooserFormMessage: return "لإخفاء عمود,\nادفعهم من جدول العرض\nعلى هذه النافذة";
                case RadGridStringId.SearchRowTextBoxNullText: return "اكتب ما تبحث عنه";
                case RadGridStringId.SearchRowChooseColumns: return "تحديد أعمدة البحث";
                case RadGridStringId.SearchRowMatchCase: return "حالة التطابق";
                case RadGridStringId.SearchRowSearchFromCurrentPosition: return "بحث في العمود الحالي";
                case RadGridStringId.GroupingPanelDefaultMessage: return "اسحب الصف و افلته هنا لتجميع البيانات بواسطه";
                case RadGridStringId.PagingPanelPagesLabel: return "صفحة";
                case RadGridStringId.PagingPanelOfPagesLabel: return "من";
                case RadGridStringId.GroupingPanelHeader: return "تجميع بواسطة";
                case RadGridStringId.SearchRowMenuItemMasterTemplate: return "القالب الرئيسي";
                case RadGridStringId.SearchRowMenuItemAllColumns: return "كافة الأعمدة";
                case RadGridStringId.SearchRowResultsOfLabel: return "من";

                case RadGridStringId.SearchRowMenuItemChildTemplates: return "البحث في قوالب فرعية لعنصر قائمة الصف";
                case RadGridStringId.NoDataText: return "لا يوجد نص بيانات";
                case RadGridStringId.HideGroupMenuItem: return "إخفاء عنصر قائمة المجموعة";
                case RadGridStringId.ExpressionMenuItem: return "عنصر قائمة التعبير";
                case RadGridStringId.ExpressionFormTooltipPlus: return "تلميح أداة نموذج التعبير Plus";
                case RadGridStringId.ExpressionFormTooltipOr: return "تلميح أداة نموذج التعبير أو";
                case RadGridStringId.ExpressionFormTooltipNotEqual: return "تلميح أداة نموذج التعبير غير متساوٍ";
                case RadGridStringId.ExpressionFormTooltipNot: return "تلميح أداة نموذج التعبير لا";
                case RadGridStringId.ExpressionFormTooltipMultiply: return "تلميح أداة نموذج التعبير ضرب";
                case RadGridStringId.ExpressionFormTooltipModulo: return "نموذج التعبير تلميح الأدوات Modulo";
                case RadGridStringId.ExpressionFormTooltipMinus: return "تلميح أداة نموذج التعبير ناقص";
                case RadGridStringId.ExpressionFormTooltipLessOrEqual: return "تلميح أداة نموذج التعبير أقل أو يساوي";
                case RadGridStringId.ExpressionFormTooltipLess: return "تلميح أداة نموذج التعبير أقل";
                case RadGridStringId.ExpressionFormTooltipGreaterOrEqual: return "تلميح أداة نموذج التعبير أكبر أو يساوي";
                case RadGridStringId.ExpressionFormTooltipGreater: return "تلميح أداة نموذج التعبير أكبر";
                case RadGridStringId.ExpressionFormTooltipEqual: return "تلميح أداة نموذج التعبير متساوي";
                case RadGridStringId.ExpressionFormTooltipDivide: return "تقسيم تلميح أداة نموذج التعبير";
                case RadGridStringId.ExpressionFormTooltipAnd: return "تلميح أداة نموذج التعبير و";
                case RadGridStringId.ExpressionFormTitle: return "عنوان نموذج التعبير";
                case RadGridStringId.ExpressionFormResultPreview: return "معاينة نتيجة نموذج التعبير";
                case RadGridStringId.ExpressionFormOrButton: return "نموذج التعبير أو الزر";
                case RadGridStringId.ExpressionFormOperators: return "مشغلي نموذج التعبير";
                case RadGridStringId.ExpressionFormOKButton: return "زر موافق لنموذج التعبير";
                case RadGridStringId.ExpressionFormNotButton: return "نموذج التعبير ليس زرًا";
                case RadGridStringId.ExpressionFormFunctionsText: return "نص وظائف نموذج التعبير";
                case RadGridStringId.ExpressionFormFunctionsOther: return "وظائف نموذج التعبير أخرى";
                case RadGridStringId.ExpressionFormFunctionsMath: return "وظائف نموذج التعبير الرياضيات";
                case RadGridStringId.ExpressionFormFunctionsLogical: return "وظائف نموذج التعبير منطقية";
                case RadGridStringId.ExpressionFormFunctionsDateTime: return "وظائف نموذج التعبير التاريخ الوقت";
                case RadGridStringId.ExpressionFormFunctionsAggregate: return "وظائف نموذج التعبير مجمعة";
                case RadGridStringId.ExpressionFormFunctions: return "وظائف نموذج التعبير";
                case RadGridStringId.ExpressionFormFields: return "حقول نموذج التعبير";
                case RadGridStringId.ExpressionFormDescription: return "وصف نموذج التعبير";
                case RadGridStringId.ExpressionFormConstants: return "ثوابت نموذج التعبير";
                case RadGridStringId.ExpressionFormCancelButton: return "زر إلغاء نموذج التعبير";
                case RadGridStringId.ExpressionFormAndButton: return "نموذج التعبير والزر";

                default:
                    return base.GetLocalizedString(id);
            }
        }
    }
}
