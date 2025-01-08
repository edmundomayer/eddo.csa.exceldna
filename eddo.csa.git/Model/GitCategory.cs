using eddo.csa.git.Enums;
using System.Data;

namespace eddo.csa.git.Model
{
    public class GitCategory
    {
        #region Fields
        private CategoryEnum _type = CategoryEnum.None;
        #endregion Fields


        #region Constructors & Destructors
        public GitCategory( string type, string alias, params string[] fileNamePatterns )
        {
            if( !Enum.TryParse( type, out _type ) )
                throw new ArgumentException( "Unknow type '{0}' received", type );

            Alias = alias;
            FileNamePatterns = fileNamePatterns?.Distinct().OrderBy( x => x ).ToArray();
        }
        #endregion Constructors & Destructors


        #region Properties
        public CategoryEnum Type { get; set; }
        public string Alias { get; set; }
        public string[] FileNamePatterns { get; set; }
        #endregion Properties
    }
}
