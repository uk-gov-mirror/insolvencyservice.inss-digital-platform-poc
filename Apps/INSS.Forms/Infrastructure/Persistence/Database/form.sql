CREATE TABLE [dbo].[form](
	[id] [uniqueidentifier] NOT NULL,
	[name] [varchar](255) NOT NULL,
	CONSTRAINT [pk_form] PRIMARY KEY CLUSTERED 
	(
		[id] ASC
	)
)
