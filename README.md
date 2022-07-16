# Digital-Thinkers-CheckOut

## Estimated Complition Time

1 hours -> Setuping up repo, project basic stuff (was a long time ago that I had to create from the begining, rusty :D)
1 hour -> Stock Endpoints and basic Architecture
1,5 hour -> Checkout Endpoint

3,5 hour + 0,5 hour testing on Postman

EUR implementation here I abandoned basic development since it was optional and it is saturday after all.
Took around 2 - 3 hours (Refactor was lengthy) with breakfast and stuff

## Testing

The documention requiremnts did not specify anywhere to write tests, so I did not.
So I would jsut summerize here what Test I would write with what priority.

High Priority Unit Tests (obvioulsy with mocking):
- Stocking only accepts HUF
- Stocking does nto accept negative coins/bills
- Stocking stocks the correct bill/coins with the correct amount
- Stock returns the correct stock
- IMoneyStockService.Checkout calculates correctly

High Priority Integration:
Just a some test type of deal just to see all layers communicate with each other and call with the right paramters

Low Prio Unit Testing:
Only accepts right parameters, throws correct errors, error filter works, correct 400 returns with correct messages

So testing was done manually and mainly to check all inputs, and some edge case wrong inputs

## Build/Run

- Build the solution in Visual Studio
- Switch to Checkout

![image](https://user-images.githubusercontent.com/109354456/179348982-dab767fd-edac-4162-8be3-f150901de9ec.png)

- Run the Application (both console, and browser will appear)

## Using The API

- The Browser will automatically will open on http://localhost:22080/api/v1/stock this will show the current stock
- Sending Post requests just use Postman extension or Desktop Application(personal preference)

### Postman Desktop Application

At Start stock up the API with [POST] : http://localhost:22080/api/v1/Stock
with json body

`
{
    "5": 10,
    "10": 10,
    "20": 10,
    "50": 10,
    "5000": 10,
    "10000": 10,
    "20000": 10
}
`
200 Response : "true"

You can check the Stock on the same url with [GET]

Also you can call http://localhost:22080/api/v1/Checkout
with json body

`
{
    "inserted":{
        "500": 1,
        "1000": 3,
    },
    "price": 3200
}
`

200 response `{"100": 1, "200": 1}`

If for some reason the API cannot give change back it returns 400
Reasons (Not enough money inserted, Not enough money in stock)

## EUR Accepting
NOTE: repository now returns the reference to the stock which could lead to problems but since this layer was always a mock and built in mind to later have DB implemented in the background, in later stages of development this issue will disappear (for now a quick solution would be to return the copy of stock)

- There is an Open PR for the changes to Accept EUR in checkout
- This was seperated to a different branch because it is a breaking change (API json model changed)
- The same Build/Run/Usage applies but with different json models

[POST] Stock
{
    "currencies": {
        "20000": 10,
        "10000": 10,
        "5000": 10,
        "2000": 10,
        "1000": 10,
        "500": 10,
        "200": 10,
        "100": 10,
        "50": 10,
        "20": 10,
        "10": 10,
        "5": 10
    },
    "currencyType": "HUF"
}

Only HUF accepted otherwise 400 response
If a value is present in the json that is not HUF bill or coin -> 400 response

[POST] Checkout
EUR 
{
    "inserted": {
        "currencies": {
            "1000": 1,
            "0.1": 1
        },
        "currencyType": "EUR"
    },
    "price": 1115
}

HUF
{
    "inserted": {
        "currencies": {
            "1000": 1,
            "2000": 1
        },
        "currencyType": "HUF"
    },
    "price": 2345
}

Any diviation form the accepted CurrencyTypes HUF, EUR -> 400 response
In currencies if the bill, coin value does not correspond to the currency type -> 400 response

## EUR accepting design
Wanted to have a dynamic json endpoint for the API so json must declare which currency we are dealing with.
If the type is not supported then through custom exception and exception filters we send back 400 response
If the bill or coin provided in the json does not exist then we throw custom error and filter is to send back 400 response

Tried to design the models so it will be easily scalable and adding new currency support will not brake the API endpoint
In hindsight would have been better to just create a new CheckoutV2 with a route V2 http://localhost:22080/api/v2/Checkout to avoid API endpoint breaking change
