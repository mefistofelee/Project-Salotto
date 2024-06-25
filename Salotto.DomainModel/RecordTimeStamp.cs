using Salotto.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Youbiquitous.Martlet.Core.Extensions;

namespace Salotto.DomainModel
{
    public class RecordTimeStamp
    {
        /// <summary>
        /// Indicates UTC time of creation of the record
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Indicates UTC time of last update on the record
        /// </summary>
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Identifier of the user who created (and owns) the record
        /// </summary>
        [MaxLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Identifier of the user who last modified the record
        /// </summary>
        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// Initializes the timestamp object
        /// </summary>
        public void Init(string author = null)
        {
            Created = DateTime.UtcNow;
            Modified = DateTime.UtcNow;
            if (!author.IsNullOrWhitespace())
            {
                CreatedBy = author;
                ModifiedBy = author;
            }
        }

        public static RecordTimeStamp Initialize(string author = null)
        {
            var record = new RecordTimeStamp();
            record.Created = DateTime.UtcNow;
            record.Modified = DateTime.UtcNow;
            if (!author.IsNullOrWhitespace())
            {
                record.CreatedBy = author;
                record.ModifiedBy = author;
            }
            return record;
        }

        /// <summary>
        /// Marks the record as modified now
        /// </summary>
        /// <param name="author"></param>
        public RecordTimeStamp Mark(string author = null)
        {
            Modified = DateTime.UtcNow;
            if (!author.IsNullOrWhitespace())
                ModifiedBy = author;
            return this;
        }

        /// <summary>
        /// Returns the date of latest update on the entity
        /// </summary>
        /// <returns></returns>
        public string LatestChangeForDisplay()
        {
            switch (Modified)
            {
                case null when !Created.HasValue:
                    return AppStrings.System_InfoNotAvailable;
                case null:
                    return Created.Value.ToStringOrEmpty("d MMM yyyy", AppStrings.System_InfoNotAvailable);
                default:
                    return Modified.Value.ToStringOrEmpty("d MMM yyyy", AppStrings.System_InfoNotAvailable);
            }
        }
    }
}
