🔑 Autenticação

Todos os endpoints protegidos exigem envio de JWT no header:

Authorization: Bearer {seu_token}

.......................................................

POST /api/auth/register/client

📌 Registro de um novo cliente.

Request body

{
  "username": "joaosilva",
  "fullName": "João da Silva",
  "email": "joao@email.com",
  "password": "123456",
  "cpfCnpj": "12345678900",
  "phoneNumber": "(11)91234-5678"
}


Response 200

{
  "message": "Cliente registrado com sucesso!",
  "id": 1
}

.......................................................

POST /api/auth/register/designer

📌 Registro de um designer.

Request body

{
  "username": "mariart",
  "fullName": "Maria Souza",
  "email": "maria@email.com",
  "password": "senhaSegura",
  "cpfCnpj": "12345678900",
  "phoneNumber": "(11)99876-5432",
  "dateOfBirth": "1995-03-15",
  "address": "Rua A, 123",
  "city": "São Paulo",
  "state": "SP",
  "zipCode": "01000-000",
  "complement": "Apto 22"
}


Response 200

{
  "message": "Designer registrado com sucesso!",
  "id": 1
}

.......................................................

POST /api/auth/login

📌 Login de cliente ou designer. Retorna token JWT com role embutida.

Request body

{
  "usernameOrEmail": "joaosilva",
  "password": "123456"
}


Response 200

{
  "id": 1,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "fullName": "João da Silva"
}

.......................................................

POST /api/auth/login/admin

📌 Login de administrador (registro é feito diretamente no banco).
(opcional implementar para TCC, mas vale mostrar a estrutura)

👤 Clientes (/api/clients)
GET /api/clients/{id}

📌 Retorna os dados de um cliente.
Autorização: Client ou Admin.

Response 200

{
  "id": 1,
  "username": "joaosilva",
  "fullName": "João da Silva",
  "email": "joao@email.com",
  "cpfCnpj": "12345678900",
  "phoneNumber": "(11)91234-5678",
  "createdAt": "2025-09-01T12:30:00Z"
}

PUT /api/clients/{id}

📌 Atualiza dados de um cliente.
Autorização: Client dono da conta ou Admin.

🎨 Designers (/api/designers)
GET /api/designers/{id}

📌 Retorna os dados de um designer.
Autorização: Designer ou Admin.

PUT /api/designers/{id}

📌 Atualiza dados de um designer.
Autorização: Designer dono da conta ou Admin.

🛠 Admin (/api/admin)

(somente role Admin pode acessar)

GET /api/admin/users

📌 Lista todos os usuários (clientes e designers).

DELETE /api/admin/users/{id}

📌 Remove um usuário do sistema.

⚡ Resumindo

Auth → Registro (client/designer) e login (client/designer/admin).

Clients → Consultar e atualizar dados.

Designers → Consultar e atualizar dados.

Admin → Listar e remover usuários.