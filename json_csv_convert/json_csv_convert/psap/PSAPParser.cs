using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace json_csv_convert.psap
{
    class PSAPParser
    {
        public static DataTable Parse(String filepath)
        {
            String json_contents = File.ReadAllText(filepath);

            //read from json
            JObject records = JObject.Parse(json_contents);

            List<String> field_array = new List<String>();
            foreach (var i in records["resources"])
            {
                foreach (JProperty j in i)
                {
                    String field = j.Name;
                    if (!field_array.Contains(field) && !field.Equals("assessment_questions"))
                    {
                        field_array.Add(field);
                    }
                }
            }

            List<String> question_array = new List<String>();
            foreach (var i in records["resources"])
            {
                foreach (var j in i["assessment_questions"])
                {
                    String question = j["question"].ToString();
                    if (!question_array.Contains(question))
                    {
                        question_array.Add(question);
                    }
                }
            }

            List<String> total_array = new List<String>();
            total_array.AddRange(field_array);
            total_array.AddRange(question_array);

            DataTable table = new DataTable();
            foreach (String s in total_array)
            {
                table.Columns.Add(s, typeof(string));
            }

            int row_count = 0;
            foreach (var i in records["resources"])
            {
                table.Rows.Add();
                foreach (JProperty j in i)
                {
                    if (!j.Name.Equals("assessment_questions"))
                    {
                        for (int k = 0; k < table.Rows[row_count].Table.Columns.Count; k++)
                        {
                            if (table.Rows[row_count].Table.Columns[k].ColumnName.Equals(j.Name))
                            {
                                if (j.Name.Equals("assessment_score_percent"))
                                {
                                    table.Rows[row_count].SetField(k, float.Parse(j.Value.ToString()) * 100);
                                }
                                else {
                                    table.Rows[row_count].SetField(k, j.Value.ToString());
                                }
                                //table.Rows[row_count].SetField(k, Regex.Replace(j.Value.ToString(), @"\t|\n|\r", ""));
                            }
                        }
                    }
                }

                foreach (JObject j in i["assessment_questions"])
                {
                    for (int k = 0; k < table.Rows[row_count].Table.Columns.Count; k++)
                    {
                        if (table.Rows[row_count].Table.Columns[k].ColumnName.Equals(j["question"].ToString()))
                        {
                            table.Rows[row_count].SetField(k, j["response"].ToString());
                            //table.Rows[row_count].SetField(k, Regex.Replace(j["response"].ToString(), @"\t|\n|\r", ""));
                        }
                    }
                }

                Console.WriteLine(String.Join(", ", table.Rows[row_count].ItemArray));

                row_count++;
            }

            //correct the column names
            Dictionary<string, string> title_replacements = new Dictionary<string, string>();

            title_replacements.Add("local_identifier", "Local Identifier");
            title_replacements.Add("name", "Title/Name");
            title_replacements.Add("assessment_score_percent", "PSAP Assessment Score");
            title_replacements.Add("assessment_type", "Assessment Type");
            title_replacements.Add("location", "Location");
            title_replacements.Add("resource_type", "Resource Type");
            title_replacements.Add("parent_resource", "Parent Resource");
            title_replacements.Add("format", "Format");
            title_replacements.Add("format_ink_media", "Format Ink/Media Type");
            title_replacements.Add("format_support", "Format Support Type");
            title_replacements.Add("significance", "Significance");
            title_replacements.Add("language", "Language");
            title_replacements.Add("rights", "Rights");
            title_replacements.Add("description", "Description");
            title_replacements.Add("created", "Created");
            title_replacements.Add("updated", "Updated");

            foreach (DataColumn v in table.Columns)
            {
                if (title_replacements.ContainsKey(v.ColumnName))
                {
                    v.ColumnName = title_replacements[v.ColumnName];
                }
            }

            return table;
        }
    }
}
