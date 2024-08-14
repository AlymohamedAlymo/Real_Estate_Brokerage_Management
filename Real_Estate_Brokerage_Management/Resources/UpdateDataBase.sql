GO

ALTER TABLE [dbo].[tbSaleOrder]
ADD lastaction varchar(255);
GO

UPDATE [dbo].[tbSaleOrder] set lastaction = ''
GO