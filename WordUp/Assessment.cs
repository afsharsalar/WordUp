using System;
using System.Collections.Generic;
using System.Linq;

namespace WordUp
{
    public class Assessment : IAssessment
    {
        /// <summary>
        /// Returns the score with the highest value
        /// </summary>
        public Score? WithMax(IEnumerable<Score> scores)
        {
            if (scores == null)
                throw new ArgumentNullException(nameof(scores));
            if (!scores.Any())
                return null;
            var max = scores.ElementAtOrDefault(0);
            for (var index = 1; index < scores.Count(); index++)
            {
                var current = scores.ElementAt(index);
                if (max.Value < current.Value)
                    max = current;
            }

            return max;
        }

        /// <summary>
        /// Returns the average value of the collection. For an empty collection it returns null
        /// </summary>
        public double? GetAverageOrDefault(IEnumerable<int> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            if (!items.Any())
                return null;

            var sum = 0;
            foreach (var item in items)
            {
                sum += item;
            }

            return sum / items.Count();
        }


        /// <summary>
        /// Appends the suffix value to the text if the text has value. If not, it returns empty.
        /// </summary>
        public string WithSuffix(string text, string suffixValue)
        {
            if (string.IsNullOrEmpty(text))
                return String.Empty;
            return $"{text}{suffixValue}";
        }

        /// <summary>
        /// It fetches all the data from the source.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public IEnumerable<Score> GetAllScoresFrom(IDataProvider<Score> source)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns child's name prefixed with all its parents' names separated by the specified separator.Example : Parent/Child
        /// </summary>
        public string GetFullName(IHierarchy child, string separator = null)
        {
            separator ??= "/";

            var parents = GetPatents(child, new List<string>());
            parents.Reverse();
            return string.Join(separator, parents);
        }

        private List<string> GetPatents(IHierarchy child, List<string> parents)
        {
            if (child != null)
                parents.Add(child.Name);
            if (child != null && child.Parent != null)
            {
                return GetPatents(child.Parent, parents);
            }

            return parents;
        }

        /// <summary>
        /// Refactor: Returns the value that is closest to the average value of the specified numbers.
        /// </summary>
        public int? ClosestToAverageOrDefault(IEnumerable<int> numbers)
        {
            //old
            //return numbers.OrderBy(x => x - numbers.Average()).First();

            //refactor
            var average = GetAverageOrDefault(numbers);
            if (average != null)
            {
                var data = numbers.ToList();
                var indexOfAverage = data.IndexOf((int)average.Value);

                if (indexOfAverage == 0)
                    indexOfAverage = 1;
                return data[indexOfAverage-1];
            }

            return null;
        }

        /// <summary>
        /// mm/dd/yyyy
        /// Groups the specified bookings based on their consecutive dates then by their projects and finally the booking allocation. Read the example carefully.
        /// Example : [{Project:HR, Date: 01/02/2020 , Allocation: 10},
        ///            {Project:CRM, Date: 01/02/2020 , Allocation: 15},
        /// 
        ///            {Project:HR, Date: 02/02/2020 , Allocation: 10},
        ///            {Project:CRM, Date: 02/02/2020 , Allocation: 15},
        /// 
        ///            {Project:HR, Date: 03/02/2020 , Allocation: 15},
        ///            {Project:CRM, Date: 03/02/2020 , Allocation: 15},
        /// 
        ///            {Project:HR, Date: 04/02/2020 , Allocation: 15},
        ///            {Project:CRM, Date: 04/02/2020 , Allocation: 15},
        /// 
        ///            {Project:HR, Date: 05/02/2020 , Allocation: 15},
        ///            {Project:CRM, Date: 05/02/2020 , Allocation: 15},
        ///            {Project:ECom, Date: 05/02/2020 , Allocation: 15},
        /// 
        ///            {Project:ECom, Date: 06/02/2020 , Allocation: 10},
        ///            {Project:CRM, Date: 06/02/2020 , Allocation: 15}
        /// 
        ///            {Project:ECom, Date: 07/02/2020 , Allocation: 10},
        ///            {Project:CRM, Date: 07/02/2020 , Allocation: 15}]    
        /// Returns : 
        ///          [
        ///            { From:01/02/2020 , To:02/02/2020 , [{ Project:CRM , Allocation:15 },{ Project:HR , Allocation:10 }]  },
        ///            { From:03/02/2020 , To:04/02/2020 , [{ Project:CRM , Allocation:15 },{ Project:HR , Allocation:15 }]  },
        ///            { From:05/02/2020 , To:05/02/2020 , [{ Project:CRM , Allocation:15 },{ Project:HR , Allocation:15 },{ Project:ECom , Allocation:15 }]  },
        ///            { From:06/02/2020 , To:07/02/2020 , [{ Project:CRM , Allocation:15 },{ Project:ECom , Allocation:10 }]  }
        ///          ]
        /// </summary>
        public IEnumerable<BookingGrouping> Group(IEnumerable<Booking> dates)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Merges the specified collections so that the n-th element of the second list should appear after the n-th element of the first collection. 
        /// Example : first : 1,3,5 second : 2,4,6 -> result : 1,2,3,4,5,6
        /// </summary>
        public IEnumerable<int> Merge(IEnumerable<int> first, IEnumerable<int> second)
        {
            var result = AddToList(first);
            result.AddRange(AddToList(second));
            return result;
        }


        private List<int> AddToList(IEnumerable<int> source)
        {
            var result = new List<int>();
            if (source != null)
            {
                foreach (var item in source)
                {
                    result.Add(item);
                }
            }
            

            return result;
        }
    }
}
