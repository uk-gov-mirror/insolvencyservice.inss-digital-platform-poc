CREATE TABLE [dbo].[form](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NOT NULL,
	[uri-path] VARCHAR(2048) NOT NULL, 
    CONSTRAINT [pk_form] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)
)
