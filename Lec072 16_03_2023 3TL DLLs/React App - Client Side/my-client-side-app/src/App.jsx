import './App.css';

const apiUrl = 'http://localhost:62742/api/users/';

function App() {

  const btnGetAllUsers = () => {

    fetch(apiUrl, {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json; charset=UTF-8',
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(res => {
        console.log('res=', res);
        console.log('res.status', res.status);
        console.log('res.ok', res.ok);
        return res.json()
      })
      .then(
        (result) => {
          console.log("fetch btnFetchGetStudents= ", result);
          result.map(user => console.log(user.Name));
          console.log('result[0].Name=', result[0].Family);
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnGetUserById = () => {

    fetch(apiUrl + '2', {
      method: 'GET',
      headers: new Headers({
        'Content-Type': 'application/json; charset=UTF-8',
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(res => {
        console.log('res=', res);
        console.log('res.status', res.status);
        console.log('res.ok', res.ok);
        return res.json()
      })
      .then(
        (result) => {
          console.log("fetch btnFetchGetStudents= ", result);
          console.log('result[0].Name=', result.Name);
        },
        (error) => {
          console.log("err post=", error);
        });
  }

  const btnPost = () => {

    const nu2Send =
    { //pay attention case sensitive!!!! should be exactly as the prop in C#!
      Id: -2,
      Name: "dora",
      Family: "bora"
    }

    fetch(apiUrl, {
      method: 'POST',
      body: JSON.stringify(nu2Send),
      headers: new Headers({
        'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
        'Accept': 'application/json; charset=UTF-8',
      })
    })
      .then(res => {
        console.log('res=', res);
        return res.json()
      })
      .then(
        (result) => {
          console.log("fetch POST= ", result);
          console.log(result.Id);
        },
        (error) => {
          console.log("err post=", error);
        });

  }

  return (
    <div className="App">
      <header className="App-header">
        Client Side <br /> <br />
        <button onClick={btnGetAllUsers}>Get All Users</button> <br />
        <button onClick={btnGetUserById}>Get User By ID</button> <br />
        <button onClick={btnPost}>POST User</button> <br />
      </header>
    </div>
  );
}

export default App;
