/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/


/* 
    NOTE:   The tables will need audit columns e.g. CreatedBy, CreatedAt, ModifiedBy, ModifiedAt
            These can be added as part of a later migration. 
*/


DECLARE @dro_id UNIQUEIDENTIFIER = 'c15c7c47-f61c-420f-9723-0d988ddf42e8'
DECLARE @dro_section_1_id UNIQUEIDENTIFIER = '7d4416e4-a2b6-4053-8844-a275f367b44a'
DECLARE @dro_section_2_id UNIQUEIDENTIFIER = 'fec429c3-84a0-4a3f-af6d-2a2b47d7a715'

DECLARE @dcrs_id UNIQUEIDENTIFIER = '75a981b3-34d5-4d3f-b011-f0c9c32a960f'
DECLARE @dcrs_section_1_id UNIQUEIDENTIFIER = '876229bc-a0a8-4ec0-94f6-b680d808e912'
DECLARE @dcrs_section_2_id UNIQUEIDENTIFIER = 'b788a6e4-466f-47db-a9bc-e02f5249d814'

DECLARE @form_about_you_id UNIQUEIDENTIFIER = 'bcb3045c-b018-47f6-b007-2c56f81b81f9'
DECLARE @form_company_details_id UNIQUEIDENTIFIER = 'e56a2c9d-88cb-4bb6-b5a5-ddc1c9192670'
DECLARE @form_individuals_income_id UNIQUEIDENTIFIER = '39c9823e-060e-4849-af43-7f40244fa586'
DECLARE @form_individuals_debt_id UNIQUEIDENTIFIER = '07000264-d088-4da1-a995-2992c38dbb53'



---------------------------------------
-- Tear Down
---------------------------------------
DELETE
FROM    [dbo].[section_form]

DELETE
FROM    [dbo].[form]

DELETE
FROM    [dbo].[digital_service_section]

DELETE
FROM    [dbo].[section]

DELETE
FROM    [dbo].[digital_service]



---------------------------------------
-- Forms
---------------------------------------
INSERT INTO [dbo].[form]
(   
    [id]
,   [name]
,   [uri-path]
)
VALUES
(
    @form_about_you_id
,   'About You'
,   'about-you'
)

INSERT INTO [dbo].[form]
(   
    [id]
,   [name]
,   [uri-path]
)
VALUES
(
    @form_company_details_id
,   'Company Details'
,   'company-details'
)

INSERT INTO [dbo].[form]
(   
    [id]
,   [name]
,   [uri-path]
)
VALUES
(
    @form_individuals_income_id
,   'Individual''s Income'
,   'individuals-income'
)

INSERT INTO [dbo].[form]
(   
    [id]
,   [name]
,   [uri-path]
)
VALUES
(
    @form_individuals_debt_id
,   'Individual`s Debts'
,   'individuals-debts'
)



---------------------------------------
-- Digital Services
---------------------------------------
INSERT INTO [dbo].[digital_service]
(   
    [id]
,   [name]
,   [title]
)
VALUES
(
    @dro_id
,   'dro.local'
,   'Debt Relief Order'
)


INSERT INTO [dbo].[digital_service]
(   
    [id]
,   [name]
,   [title]
)
VALUES
(
    @dcrs_id
,   'dcrs.local'
,   'Director Conduct Reporting Service'
)



---------------------------------------
-- Sections
---------------------------------------
INSERT INTO [dbo].[section]
(   
    [id]
,   [name]
,   [description]
)
VALUES
(
    @dro_section_1_id
,   'Personal Information'
,   'Please provide your personal information to proceed with the application.'
)       

INSERT INTO [dbo].[section]
(   
    [id]
,   [name]
,   [description]
)
VALUES
(
    @dro_section_2_id
,   'Financial Information'
,   'Please provide your financial information to proceed with the application.'
)       

INSERT INTO [dbo].[section]
(   
    [id]
,   [name]
,   [description]
)
VALUES
(
    @dcrs_section_1_id
,   'Your Information'
,   'Please provide your information to proceed with the application.'
)       

INSERT INTO [dbo].[section]
(   
    [id]
,   [name]
,   [description]
)
VALUES
(
    @dcrs_section_2_id
,   'Your Salary & Income'
,   'Please provide your company details and income information.'
)       



---------------------------------------
-- Digital Service Sections
---------------------------------------
INSERT INTO [dbo].[digital_service_section]
(   
    [digital_service_id]
,   [section_id]
,   [sort_order]
)
VALUES
(
    @dro_id
,   @dro_section_1_id
,   1
)

INSERT INTO [dbo].[digital_service_section]
(   
    [digital_service_id]
,   [section_id]
,   [sort_order]
)
VALUES
(
    @dro_id
,   @dro_section_2_id
,   2
)

INSERT INTO [dbo].[digital_service_section]
(   
    [digital_service_id]
,   [section_id]
,   [sort_order]
)
VALUES
(
    @dcrs_id
,   @dcrs_section_1_id
,   1
)

INSERT INTO [dbo].[digital_service_section]
(   
    [digital_service_id]
,   [section_id]
,   [sort_order]
)
VALUES
(
    @dcrs_id
,   @dcrs_section_2_id
,   2
)



---------------------------------------
-- Section Forms
---------------------------------------
INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dro_section_1_id
,   @form_about_you_id
,   1
)

INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dro_section_1_id
,   @form_company_details_id
,   2
)

INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dro_section_2_id
,   @form_individuals_income_id
,   1
)

INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dro_section_2_id
,   @form_individuals_debt_id
,   2
)


INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dcrs_section_1_id
,   @form_about_you_id
,   1
)

INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dcrs_section_2_id
,   @form_company_details_id
,   1
)

INSERT INTO [dbo].[section_form]
(   
    [section_id]
,   [form_id]
,   [sort_order]
)
VALUES
(
    @dcrs_section_2_id
,   @form_individuals_income_id
,   2
)

