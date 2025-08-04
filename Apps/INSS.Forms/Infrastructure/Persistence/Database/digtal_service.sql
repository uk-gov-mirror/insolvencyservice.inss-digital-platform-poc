CREATE TABLE [dbo].[digital_service](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NOT NULL,
	CONSTRAINT [pk_digital_service] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)
)
