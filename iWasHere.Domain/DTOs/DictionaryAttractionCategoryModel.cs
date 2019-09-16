using iWasHere.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace iWasHere.Domain.DTOs
{
    public class DictionaryAttractionCategoryModel
    {
        private DictionaryAttractionCategory dictionaryAttractionCategory;

        public int AttractionCategoryId { get; set; }
        public string AttractionCategoryName { get; set; }

        public DictionaryAttractionCategoryModel(DictionaryAttractionCategory dictionaryAttractionCategory)
        {
            this.dictionaryAttractionCategory = dictionaryAttractionCategory;
        }

        
    }
}
