


const CheckPassword = async () => { 
  
    const password = document.getElementById("Password").value;
    try {
        const responsePost = await fetch("api/Users/checkPassword", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(password)
        });
        const result = await responsePost.json()
        if (responsePost.ok) {
            alert(result)
           return result;
       }
    }
    catch (e) {

    }
    ;
}