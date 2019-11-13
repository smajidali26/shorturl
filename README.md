# shorturl

Application is build using asp.net core 3.0 and visual studio 2019

Open SQL Management studio and create database with name of ShortUrl

Open new query window for ShortUrl database

Execute following script to create table

#Script

GO

CREATE TABLE [dbo].[UrlEntity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [varchar](max) NOT NULL,
	[Url] [varchar](max) NOT NULL,
	[DateCreated] [datetime] NOT NULL,
 CONSTRAINT [PK_UrlEntity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO

ALTER TABLE [dbo].[UrlEntity] ADD  CONSTRAINT [DF_UrlEntity_DateCreated]  DEFAULT (getdate()) FOR [DateCreated]
GO

#Script

#Change DB Path in Code

Open Code in VS 2019

Go to ShortUrl website and open appsettings.json file

Update "ShortUrl": "Server=.;Database=ShortUrl;Integrated Security=true;"  with your DB server name.

Run Application and It will take you to {Host}/Home/Index page

#Live Application
Live Application is running on following URL https://shorturltest.azurewebsites.net/Home/Index
