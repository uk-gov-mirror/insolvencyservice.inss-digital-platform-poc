CREATE TABLE [dbo].[step_form](
	[step_id] [uniqueidentifier] NOT NULL,
	[form_id] [uniqueidentifier] NOT NULL,
	sort_order [int] NOT NULL,
	CONSTRAINT [pk_step_form] PRIMARY KEY CLUSTERED 
	(
		[step_id] ASC,
		[form_id] ASC
	)
)
GO

ALTER TABLE [dbo].[step_form] ADD CONSTRAINT [fk_step_form_step] FOREIGN KEY([step_id])
REFERENCES [dbo].[step] ([id])
GO

ALTER TABLE [dbo].[step_form] CHECK CONSTRAINT [fk_step_form_step]
GO

ALTER TABLE [dbo].[step_form]  ADD CONSTRAINT [fk_step_form_form] FOREIGN KEY([form_id])
REFERENCES [dbo].[form] ([id])
GO

ALTER TABLE [dbo].[step_form] CHECK CONSTRAINT [fk_step_form_form]
GO



