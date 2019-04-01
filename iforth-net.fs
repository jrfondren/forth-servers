needs -dynlibs

Library: ./libserverlib.so
AliasedExtern: server-socket int server_socket ( int port );
AliasedExtern: accept-client int accept_client ( int fd );
Extern: void reap (void);
AliasedExtern: pollfd.fd! void set_pollfd_fd ( int fd, struct pollfds *pfd ); 
AliasedExtern: pollfd.fd@ int get_pollfd_fd ( struct pollfds *pfd );
AliasedExtern: 0pollfd void init_pollfd ( struct pollfds *pfd );
AliasedExtern: pollfd-bad? int is_pollfd_bad ( struct pollfds *pfd );
AliasedExtern: pollfd-ready? int is_pollfd_ready ( struct pollfds *pfd );

Library: libc.so.6
Extern: int send ( int fd, void *buf, int len, int flags );
Extern: int recv ( int fd, void *buf, int len, int flags );
AliasedExtern: close-socket int close ( int fd );
Extern: int fork (void);
Extern: int poll ( struct pollfds *fds, int nfds, int timeout );
Extern: int wtf_int (void);


$1 constant POLLIN
$8 constant POLLERR
$10 constant POLLHUP
$20 constant POLLNVAL

8 constant pollfd%

: ?oops ( n -- n )
   dup 0< abort" something failed :(" ;
