# MonoNLayer
## C# project template for N-Layer type architecture

### Features

* WebAPI
  * OWIN compatible
  * AutoFac dependancy resolver
* ASP.NET Identity
  * UserManager fully MySQL compatible
  * MySQL setup SQL included
* OAuth2 bearer token authentication
* Tests 

### Available routes

#### Register
```
curl localhost:9000/api/accounts/register -X POST -d '{"email":"newuser5@email.com", "password": "123456"}' -H 'Content-type: application/json'
{
  "message": "Success"
}
```

#### Login
Notice, body must be url encoded
```
curl localhost:8080/token -X POST -d 'grant_type=password&username=newuser@email.com&password=123456'
{
   "access_token":"9-gVL5t7nbH8L5imKQqt6JqWflxSBjof_cHbchabi90gx-mxiyOH66QIS8A43tmj2MZFe9eZlU5ugf0-c9wDRy9AAJUAWBZMn1_LxPhrD-BTg7x3yAlwyXS5a1DSrhB7HD3xAbMkbSIu7Z18oIbppifOxNXnoAjjwyqikqxqPfi1svOkDmDKAGa7Bz_4PhYqxUnUiqMUn5Gmzh4B8dT_cCGeqkCRkpT2Bs9mYPgfDRR5zL3nu6wOthQB1cnzGp3g2hxnAnibz3Kf9pkOY7OHMlQsNAmT5pk1vuk7-tgnpl3FxDLJCxXg7x8y151PHmu3UQD3lBaeR65BUP-5W5El4g",
   "token_type":"bearer",
   "expires_in":1209599
}
```

#### Who am I
```
curl localhost:8080/api/accounts -X GET -H 'Content-type: application/json' -H 'Authorization: Bearer 8kdLng13bzQX0WqtsMGSIoFktdr5-LOnrV9apet0A4BslgLas7JYscQd_zVWwGNQBdAnioQBNkJELwOy7L94NKGiTnliM7jK6R5IgqX80DiaFXRqwfy3x4nS6DChRH81SqDPgRW9qifwYMyaIIDWhW3I8-g-C3WNCAtmsVVTXnXwaG4BLlyJECV-Cx5rBp4hrqtVnq19hwEjv8e3dA0_5_trQiFGqHn7evcS0obcevW6OL1U5SZLcJDEhYV5LuZKIFRjqHBN3FjSOYWBYxkgy1zMEZMfC819mEYbQv0m6yEGiCoXxV_Ii0IlLCLhUsmALuFNCbpruPtsItpWlanNqhyMaWS-kP8VO5eqHx9sYveexHsRDqzmwSdbJreOHh-Jy0Rx3vCIhub-DTNA3OJtF5SpNBTlJAZo7yCqXD0KkqVdt9U44B9KOQOzdCbHX87rl1_ewG_vjrbT0y3T1-qe2Nq4thybt-Po8DB1p61I5g'
{
  "userName": "newuser@email.com",
  "userId": "c752f9a1-e345-4cd2-b00a-58823727fefa",
  "isAuthenticated": true,
  "authenticationType": "Bearer"
}
```

#### Protected Resource
```
curl localhost:8080/api/secret -X GET -H 'Content-type: application/json' -H 'Authorization: Bearer 7aUHA3xFxJQNO4Zh_oq3TaqFc74_XaeMgHTj9-4f5m6fBEoahEGR3LmXQ5HZUnAJb5k1xD7J6XoQxJERHbo0o2Udi7-5UM8ul7SEwn15a8zEFlE5xyksYgddTGmrt_Ny1bXx7KK0MYsosAzNIqXxQztGzHPtUsH7nKxYJHCa4LtiwaN8G5ejMoa5NcTjUSBa-re4_15Jtu0MvmqddNXcPFwjztM1daA75lhnCfyu6L30Jkj5L9N0hmFTovIE9KLpTnyfJhW82C6QWxupZO23eaWZ_vIdZ-3NTLk0YR4VAoJOiCDBGOdXTDafm8RWD8NGWTt-2SoJwo_BQP1s9wHhPF93X_J9aWdaqhzkdkDWhI41MWOAsmonmfYuH9nHYO5HFPLy7PsHZLUeuXmSf9uHDrdzap2iS3Yv7_nQNTIUsoFEuz_xibNDtBThv5x1exCt7pRNpD0O_tIXFh8JHUiHsqi9xIcTYXPy0dzhOFcykRo'
{
  "message": "This is a secret. Use it to test authorization"
}
```
