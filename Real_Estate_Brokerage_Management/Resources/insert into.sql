
DELETE FROM dataNew.[dbo].tbAccount
INSERT INTO dataNew.[dbo].tbAccount
SELECT * FROM dataOld.[dbo].tbAccount
Go

DELETE FROM dataNew.[dbo].tbLog
INSERT INTO dataNew.[dbo].tbLog
SELECT * FROM data2024.[dbo].tbLog
Go

DELETE FROM dataNew.[dbo].tbAgent
INSERT INTO dataNew.[dbo].tbAgent
SELECT  *, ' ' FROM dataOld.[dbo].tbAgent
Go

DELETE FROM dataNew.[dbo].tbAppInfo
INSERT INTO dataNew.[dbo].tbAppInfo
SELECT * FROM dataOld.[dbo].tbAppInfo
Go

DELETE FROM dataNew.[dbo].tbAttachment
INSERT INTO dataNew.[dbo].tbAttachment
SELECT * FROM dataOld.[dbo].tbAttachment
Go

DELETE FROM dataNew.[dbo].tbBanks
INSERT INTO dataNew.[dbo].tbBanks
SELECT * FROM dataOld.[dbo].tbBanks
Go

DELETE FROM dataNew.[dbo].tbBillBody
INSERT INTO dataNew.[dbo].tbBillBody
SELECT * FROM dataOld.[dbo].tbBillBody
Go

DELETE FROM dataNew.[dbo].tbBillheader
INSERT INTO dataNew.[dbo].tbBillheader
SELECT *, ' ' FROM dataOld.[dbo].tbBillheader
Go

DELETE FROM dataNew.[dbo].tbEditPassword
INSERT INTO dataNew.[dbo].tbEditPassword
SELECT * FROM dataOld.[dbo].tbEditPassword
Go

DELETE FROM dataNew.[dbo].tbEmailSettings
INSERT INTO dataNew.[dbo].tbEmailSettings
SELECT * FROM dataOld.[dbo].tbEmailSettings
Go

DELETE FROM dataNew.[dbo].tbPlanInfo
INSERT INTO dataNew.[dbo].tbPlanInfo
SELECT * FROM dataOld.[dbo].tbPlanInfo
Go

DELETE FROM dataNew.[dbo].tbLand
INSERT INTO dataNew.[dbo].tbLand
SELECT (SELECT TOP(1) guid From dataOld.[dbo].tbPlanInfo),  *, ' ' FROM dataOld.[dbo].tbLand
Go

DELETE FROM dataNew.[dbo].tbLandTrans
INSERT INTO dataNew.[dbo].tbLandTrans
SELECT * FROM dataOld.[dbo].tbLandTrans
Go

DELETE FROM dataNew.[dbo].tbPay
INSERT INTO dataNew.[dbo].tbPay
SELECT * FROM dataOld.[dbo].tbPay
Go

DELETE FROM dataNew.[dbo].tbPayContract
INSERT INTO dataNew.[dbo].tbPayContract
SELECT * FROM dataOld.[dbo].tbPayContract
Go

DELETE FROM dataNew.[dbo].tbPaymentsNotes
INSERT INTO dataNew.[dbo].tbPaymentsNotes
SELECT * FROM dataOld.[dbo].tbPaymentsNotes
Go

DELETE FROM dataNew.[dbo].tbPicture
INSERT INTO dataNew.[dbo].tbPicture
SELECT * FROM dataOld.[dbo].tbPicture
Go


DELETE FROM dataNew.[dbo].tbPriceLog
INSERT INTO dataNew.[dbo].tbPriceLog
SELECT * FROM dataOld.[dbo].tbPriceLog
Go

DELETE FROM dataNew.[dbo].tbReportCustomView
INSERT INTO dataNew.[dbo].tbReportCustomView
SELECT * FROM dataOld.[dbo].tbReportCustomView
Go

DELETE FROM dataNew.[dbo].tbSaleOrder
INSERT INTO dataNew.[dbo].tbSaleOrder
SELECT * FROM dataOld.[dbo].tbSaleOrder
Go

DELETE FROM dataNew.[dbo].tbSaleOrderBody
INSERT INTO dataNew.[dbo].tbSaleOrderBody
SELECT * FROM dataOld.[dbo].tbSaleOrderBody
Go

DELETE FROM dataNew.[dbo].tbSelectObject
INSERT INTO dataNew.[dbo].tbSelectObject
SELECT * FROM dataOld.[dbo].tbSelectObject
Go

DELETE FROM dataNew.[dbo].tbSMSsettings
INSERT INTO dataNew.[dbo].tbSMSsettings
SELECT * FROM dataOld.[dbo].tbSMSsettings
Go

DELETE FROM dataNew.[dbo].tbTaxDiscount
INSERT INTO dataNew.[dbo].tbTaxDiscount
SELECT * FROM dataOld.[dbo].tbTaxDiscount
Go

DELETE FROM dataNew.[dbo].tbUsers
INSERT INTO dataNew.[dbo].tbUsers
SELECT * FROM dataOld.[dbo].tbUsers
Go

DELETE FROM dataNew.[dbo].tbUsersPermissions
INSERT INTO dataNew.[dbo].tbUsersPermissions
SELECT * FROM dataOld.[dbo].tbUsersPermissions
Go

DELETE FROM dataNew.[dbo].tbVatSettings
INSERT INTO dataNew.[dbo].tbVatSettings
SELECT * FROM dataOld.[dbo].tbVatSettings
Go
