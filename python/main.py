import sys
import json
import csv
from _datetime import datetime

def main():
    source_file = sys.argv[0]
    destination_file = sys.argv[1]

    #handle arguments
    if sys.argv.__len__() > 3:
        if '/s' in sys.argv:
            source_file = sys.argv[sys.argv.index('/s') + 1]
        if '/d' in sys.argv:
            destination_file = sys.argv[sys.argv.index('/d') + 1]
    elif sys.argv.__len__() == 3:
        source_file = sys.argv[1]
        destination_file = sys.argv[2]
    else:
        source_file = sys.argv[1]
        destination_file = datetime.now().strftime("%Y-%m-%d_%p-%I-%M-%S") + ".csv"

    #open the json
    json_contents = json.loads(open(source_file, 'r').read())

    #get the initial fields from each record in the json
    field_array = []
    for i in json_contents["resources"]:
        for j in i:
            if not j in field_array:
                field_array.append(j)

    #get the questions from the json
    question_array = []
    for i in json_contents["resources"]:
        for j in i["assessment_questions"]:
            if not j["question"] in question_array:
                question_array.append(j["question"])

    #concat the arrays
    total_array = field_array + question_array

    result_arrays = []
    for i in json_contents["resources"]:
        #for each item
        result_array = [''] * total_array.__len__()
        for n in total_array:
            try:
                if i[n] is '[]':
                    result_array[total_array.index(n)] = ''
                else:
                    result_array[total_array.index(n)] = i[n]
            except KeyError:
                result_array[total_array.index(n)] = ''
        print(result_array)

        # for j in i:
        #     print(i[j])

    title_array = [
        "Local Identifier",
        "Title/Name",
        "PSAP Assessment Score",
        "Assessment Type",
        "Location",
        "Resource Type",
        "Parent Resource",
        "Format",
        "Format Ink/Media Type",
        "Format Support Type",
        "Significance",
        "Language",
        "Rights",
        "Description",
        "Created",
        "Updated"
    ] + question_array

    item_array = []
    item_array.append(title_array)

    for i in json_contents["resources"]:

        answer_array = [' '] * question_array.__len__()
        try:
            for n in question_array:
                for m in i["assessment_questions"]:
                    if m["question"] in question_array:
                        answer_array[question_array.index(m["question"])] = m["response"]
        except KeyError:
            print()

        str_format = ''
        try:
            str_format = i["format"]
        except KeyError:
            print("Missing 'format' column.")

        item = [
                i["local_identifier"],
                i["name"],
                float(i["assessment_score_percent"]) * 100,
                i["assessment_type"],
                i["location"],
                i["resource_type"],
                ' ', #TODO Parent Resource
                str_format,
                ' ', #TODO Format Ink/Media Type
                ' ', #TODO Format Support Type
                i["significance"],
                ' ', #TODO Language
                i["rights"],
                i["description"],
                i["created"],
                i["updated"]
                ] + answer_array
        item_array.append(item)

    # https://docs.python.org/2/library/csv.html #note to self python 3, not 2

    print(item_array)

    with open(destination_file, 'w', newline='') as file:
        writer = csv.writer(file, quoting=csv.QUOTE_ALL)
        writer.writerows(item_array)

        # for i in item_array:
        #     writer.writerows(i.split())


def print_json(json_string):
    print(json.dumps(json_string, indent=2, sort_keys=True))


if __name__ == '__main__':
    main()
