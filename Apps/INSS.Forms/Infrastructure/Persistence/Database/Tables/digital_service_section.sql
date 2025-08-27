CREATE TABLE [dbo].[digital_service_section](
	[digital_service_id] [uniqueidentifier] NOT NULL,
	[section_id] [uniqueidentifier] NOT NULL,
	[sort_order] [int] NOT NULL,
	CONSTRAINT [pk_digital_service_section] PRIMARY KEY CLUSTERED 
	(
		[digital_service_id] ASC,
		[section_id] ASC
	)
)
GO

ALTER TABLE [dbo].[digital_service_section] ADD CONSTRAINT [fk_digital_service_section_digital_service] FOREIGN KEY([digital_service_id])
REFERENCES [dbo].[digital_service] ([id])
GO

ALTER TABLE [dbo].[digital_service_section] CHECK CONSTRAINT [fk_digital_service_section_digital_service]
GO

ALTER TABLE [dbo].[digital_service_section]  ADD CONSTRAINT [fk_digital_service_section_section] FOREIGN KEY([section_id])
REFERENCES [dbo].[section] ([id])
GO

ALTER TABLE [dbo].[digital_service_section] CHECK CONSTRAINT [fk_digital_service_section_section]
GO



