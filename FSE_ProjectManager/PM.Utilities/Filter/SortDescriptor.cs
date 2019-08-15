namespace PM.Utilities.Filter
{
    public enum SortDirection
    {
        Undefined = 0,
        ASC = 1,
        DSC = 2
    }

    public class SortDescriptor
    {
        /// <summary>
        /// The field that is sorted.
        /// </summary>
        public string Field { get; set; }

        public string Dir { get; set; }

        /// <summary>
        /// The sort direction.
        ///
        /// If no direction is set, the descriptor will be skipped during processing.
        /// </summary>
        public SortDirection Direction
        {
            get
            {
                if (!string.IsNullOrEmpty(Dir))
                {
                    switch (Dir.Trim().ToLower())
                    {
                        case "asc":
                            return SortDirection.ASC;

                        case "desc":
                            return SortDirection.DSC;

                        default:
                            throw new System.InvalidOperationException();
                    }
                }

                return SortDirection.Undefined;
            }
        }
    }
}