## Articles Api Project

# Requirements
You will be required to create a simple API with three endpoints.

The first endpoint, POST /articles should handle the receipt of some article data in json format, and store it within the service.

The second endpoint GET /articles/{id} should return the JSON representation of the article.

The final endpoint, GET /tags/{tagName}/{date} will return the list of articles that have that tag name on the given date and some summary data about that tag for that day.

An article has the following attributes id, title, date, body, and list of tags. for example:

{
  "id": "1",
  "title": "latest science shows that potato chips are better for you than sugar",
  "date" : "2016-09-22",
  "body" : "some text, potentially containing simple markup about how potato chips are great",
  "tags" : ["health", "fitness", "science"]
}
The GET /tag/{tagName}/{date} endpoint should produce the following JSON. Note that the actual url would look like /tags/health/20160922.

{
  "tag" : "health",
  "count" : 17,
    "articles" :
      [
        "1",
        "7"
      ],
    "related_tags" :
      [
        "science",
        "fitness"
      ]
}
The related_tags field contains a list of tags that are on the articles that the current tag is on for the same day. It should not contain duplicates.

The count field shows the number of tags for the tag for that day.

The articles field contains a list of ids for the last 10 articles entered for that day.

========================================================================================
# Instructions to get this going
  ## bash
    -> ensure postgres is installed
    -> run psql -f postgres-init.sql
    -> ensure dotnet sdk 2.1.302 is installed https://www.microsoft.com/net/download
    -> cd to Articles.Db
        -> run dotnet restore
        -> Apply migrations: dotnet ef database update --startup-project ../Articles.Api
    -> cd to back to Articles.Api
        -> run dotnet restore
        -> run dotnet run

  ## Docker (almost there, ran out of time for now)
      -> Install docker and docker-compose
      -> cd to articles directory
      -> run docker-compose build
      -> In a seperate terminal window run:
                docker run -d --name postgres
      -> Run docker run web