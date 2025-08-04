CREATE TABLE [dbo].[digital_service_step](
	[digital_service_id] [uniqueidentifier] NOT NULL,
	[step_id] [uniqueidentifier] NOT NULL,
	[sort_order] [int] NOT NULL,
	CONSTRAINT [pk_digital_service_step] PRIMARY KEY CLUSTERED 
	(
		[digital_service_id] ASC,
		[step_id] ASC
	)
)
GO

ALTER TABLE [dbo].[digital_service_step] ADD CONSTRAINT [fk_digital_service_step_digital_service] FOREIGN KEY([digital_service_id])
REFERENCES [dbo].[digital_service] ([id])
GO

ALTER TABLE [dbo].[digital_service_step] CHECK CONSTRAINT [fk_digital_service_step_digital_service]
GO

ALTER TABLE [dbo].[digital_service_step]  ADD CONSTRAINT [fk_digital_service_step_step] FOREIGN KEY([step_id])
REFERENCES [dbo].[step] ([id])
GO

ALTER TABLE [dbo].[digital_service_step] CHECK CONSTRAINT [fk_digital_service_step_step]
GO



