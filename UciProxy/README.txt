
Config:

        --------
        | GUI  |
        --------
        /       \
       /         \                  Other Machine
    [ Proxy1 ]   [ Proxy2 ] <=====> [Proxy3]
    [ Exe1 ]                        [Exe2]

- Proxy1
input  = exe
output = console

- Proxy2
input = network
output = network

- Proxy3
input = exe
output = network

input/output type = console / exe / network
A mettre en config (type + addresse)

exe, adress = path to exe
console, adress unused
network, adress = machine:port
