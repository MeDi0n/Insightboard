<!-- F ->> B: frontend make a request to backend -->

**POST**
make a request for data

**/dashboards**
create task

**Request**
body and settings

**Response**
status 202, id

<!-- F ->> B: is response {id} ready? -->

**GET**
polling

**/dashboards/{id}**
gives task status, when success - spec

**Request**
/dashboards/{id}

**Response**
status processing - 200 OK, {"status": "processing"}
status success - 200 OK, {"status": "done", spec}
status failed - 200 OK, {"status": "failed"}
id not found - status 404
