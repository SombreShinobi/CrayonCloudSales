# CrayonCloudSales

## Setup

1. Clone the repository
2. Make sure you have MSSQL installed
3. Run it using Visual Studio
4. The application will start listening on `http://localhost:5238`


## Notes

 - Due to time-constraints I wasn't able to implement any sort of testing.
 - `customerId` was used as a pseudo-authentication to verify the identity of the API caller (in a real application this would most likely come from the authentication token)

### Endpoints

```
GET /api/accounts?customerId={customerId} // get all accounts for a single customer
GET /api/accounts/{accountId}?customerId={customerId} // get a single account

GET /api/softwareservices // list all services available for purchase

GET /api/softwarelicenses/account/{accountId}?customerId={customerId} // get all software licenses for a single account
GET /api/softwarelicenses/{licenseId}?customerId={customerId} // get details of a single purchased license
POST /api/softwarelicenses/order // order a software license
PUT /api/softwarelicenses/{licenseId}/quantity // update software license quantity
PUT /api/softwarelicenses/{licenseId}/cancel // cancel an active software license
PUT /api/softwarelicenses/{licenseId}/activate // activate a cancelled software license
PUT /api/softwarelicenses/{licenseId}/extend // extend a software license
```
