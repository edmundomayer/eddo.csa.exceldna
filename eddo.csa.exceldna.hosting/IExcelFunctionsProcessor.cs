using ExcelDna.Registration;

namespace eddo.csa.exceldna.hosting
{
    public interface IExcelFunctionsProcessor
    {
        IEnumerable<ExcelFunctionRegistration> Process( IEnumerable<ExcelFunctionRegistration> registrations );
    }
}
