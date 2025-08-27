CREATE TABLE [dbo].[section_form](
	[section_id] [uniqueidentifier] NOT NULL,
	[form_id] [uniqueidentifier] NOT NULL,
	sort_order [int] NOT NULL,
	CONSTRAINT [pk_section_form] PRIMARY KEY CLUSTERED 
	(
		[section_id] ASC,
		[form_id] ASC
	)
)
GO

ALTER TABLE [dbo].[section_form] ADD CONSTRAINT [fk_section_form_section] FOREIGN KEY([section_id])
REFERENCES [dbo].[section] ([id])
GO

ALTER TABLE [dbo].[section_form] CHECK CONSTRAINT [fk_section_form_section]
GO

ALTER TABLE [dbo].[section_form]  ADD CONSTRAINT [fk_section_form_form] FOREIGN KEY([form_id])
REFERENCES [dbo].[form] ([id])
GO

ALTER TABLE [dbo].[section_form] CHECK CONSTRAINT [fk_section_form_form]
GO



