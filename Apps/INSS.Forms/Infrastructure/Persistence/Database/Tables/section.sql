CREATE TABLE [dbo].[section]
(
	[id] UNIQUEIDENTIFIER NOT NULL,
	[name] VARCHAR(255) NOT NULL,
	[description] VARCHAR(1000) NULL,
	CONSTRAINT [pk_section] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)
)
