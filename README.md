# Sistema de Gerenciamento de Palestras 📜
- Este é um aplicativo de console desenvolvido em C# para gerenciar uma conferência de programação. Ele organiza várias trilhas com sessões matinais e vespertinas, garantindo que as sessões comecem e terminem no horário, e que haja um evento de networking no final do dia.
- Observação: "Lectures" correspondem a "Palestras" e "Tracks" correspondem a Trilhas"

# Regras de negócio
- Múltiplas Trilhas: A conferência tem múltiplas trilhas, cada uma com uma sessão matutina e uma sessão vespertina.
- Várias Palestras: Cada sessão possui várias palestras.
- Horários das Sessões: As sessões matutinas começam às 9 da manhã e precisam encerrar ao meio-dia, para o almoço. As sessões vespertinas começam às 1 da tarde e devem encerrar a tempo do evento de networking.
- Evento de Networking: O evento de networking não pode começar antes das 4 da tarde, nem depois das 5 da tarde.
- Duração das Palestras: As durações das palestras estão descritas em minutos ou como relâmpago (5 minutos).

# Requisitos
.NET 8 instalado, para baixar acesse esse link: [dotnet 8.0](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

## 🚀 Rodando o sistema

1. Execute o seguinte comando para clonar o projeto:

```sh
git clone https://github.com/migbrit/lecture-management.git
```
2. Navegue até a pasta raiz do projeto.

3. O arquivo "lectures.txt" está dentro da pasta nomeada como "Data", nesse arquivo é possível editar as palestras da conferência e suas respectivas durações.

4. Abra um terminal ou prompt de comando.

5. Execute o seguinte comando para compilar o projeto:

```sh
dotnet build
```

6. Execute o seguinte comando para executar o projeto:

```sh
dotnet run 
```

# Exemplo de entrada

```sh
Writing Fast Tests Against Enterprise .Net 60min
Overdoing it in Python 45min
Lua for the Masses 30min
.Net Errors from Mismatched Nuget Versions 45min
.Net Core: Why We Should Move On 60min
Common .Net Errors 45min
Pair Programming vs Noise 45min
Programming in the Boondocks of Seattle 30min
.Net vs. Clojure for Back-End Development 30min
User Interface CSS in .Net Apps 30min
Communicating Over Distance 60min
.Net Magic 60min
Woah 30min
Sit Down and Write 30min
Accounting-Driven Development 45min
Clojure Ate Scala (on my project) 45min
A World Without HackerNews 30min
.Net Core Legacy App Maintenance 60min
Python for .Net Developers relâmpago

```

# Exemplo de saída

```sh
Trilha 1:
09:00H Writing Fast Tests Against Enterprise .Net 60min
10:00H Overdoing it in Python 45min
10:45H Lua for the Masses 30min
11:15H .Net Errors from Mismatched Nuget Versions 45min
12:00H Almoço
13:00H .Net Core: Why We Should Move On 60min
14:00H Common .Net Errors 45min
14:45H Pair Programming vs Noise 45min
15:30H Programming in the Boondocks of Seattle 30min
16:00H .Net vs. Clojure for Back-End Development 30min
16:30H User Interface CSS in .Net Apps 30min
17:00H Networking Event

Trilha 2:
09:00H Communicating Over Distance 60min
10:00H .Net Magic 60min
11:00H Woah 30min
11:30H Sit Down and Write 30min
12:00H Almoço
13:00H Accounting-Driven Development 45min
13:45H Clojure Ate Scala (on my project) 45min
14:30H A World Without HackerNews 30min
15:00H .Net Core Legacy App Maintenance 60min
16:00H Python for .Net Developers relâmpago
16:05H Networking Event
```

# Rodando os Testes de Unidade e Integração
1. Abra um terminal ou prompt de comando.

2. Navegue até a pasta raiz do projeto.

3. Execute o seguinte comando para executar os testes de unidade e integração

```sh
dotnet test
```
