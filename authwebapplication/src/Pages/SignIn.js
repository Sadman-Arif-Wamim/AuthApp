import axios from 'axios';
import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import Backdrop from '@mui/material/Backdrop';
import CircularProgress from '@mui/material/CircularProgress';
import MuiAlert from '@mui/material/Alert';
import Snackbar from '@mui/material/Snackbar'

function Copyright(props) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="">
                Authentication Application
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const defaultTheme = createTheme();

const Alert = React.forwardRef(function Alert(props, ref) {
    return <MuiAlert elevation={6} ref={ref} variant="filled" {...props} />;
});



export default function SignIn() {

    const [formData, setFormData] = useState({ userName: '', password: '' });
    const [open, setOpen] = React.useState(false);
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [message, setMessage] = React.useState('');
    const [severity, setSeverity] = React.useState('');

    const handleClose = () => {
        setOpen(false);
    };
    const handleOpen = () => {
        setOpen(true);
    };

    const handleSnackbarOpen = () => {
        setShowSnackbar(true);
    }
    const handleSnackbarClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }

        setShowSnackbar(false);
    }

    const handleAuth = (formData) => {
        var token = '';
        var id = 0;
        var role = '';

        const tokenResponse = axios
            .post('https://localhost:7225/api/Authetication/authenticate',
                formData)
            .then(function (response) {
                console.log(response.data);
                token = response.data.token;
                id = response.data.id;
                role = response.data.role;
                console.log(id, role);

                const token_config = {
                    headers: {
                        'Authorization': `Bearer ${token}`,
                    }
                }

                if (id === 1 && role === 'admin') {
                    const detailsResponse = axios
                        .get('https://localhost:7225/api/UserDetails/getAllDetails/' + id, token_config)
                        .then(function (response) {
                            console.log(response.data)
                            if (response.data.userName !== null || response.data.userName !== '') {
                                console.log('admin success')
                                handleClose(); 
                                handleSnackbarOpen();
                                setMessage('Authenticated and Authorized Admin!')
                                setSeverity('success');
                            }
                            else {
                                console.log('wrong token')
                                handleClose();
                                handleSnackbarOpen();
                                setMessage('Unauthorized!')
                                setSeverity('warning');

                            }
                        })
                }
                else {
                    const detailsResponse = axios
                        .get('https://localhost:7225/api/UserDetails/getRegularDetails/' + id, token_config)
                        .then(function (response) {
                            if (response.data.userName !== null || response.data.userName !== '') {
                                console.log('regular success')
                                handleClose();
                                handleSnackbarOpen();
                                setMessage('Authenticated and Authorized User!')
                                setSeverity('success');
                            }
                            else {
                                console.log('Not found')
                                handleClose();
                                handleSnackbarOpen();
                                setMessage('Unauthorized!')
                                setSeverity('warning');
                            }
                        })
                } 
            })
            .catch(function (error) {
                console.log(error)
                handleClose();
                handleSnackbarOpen();
                setMessage('Unauthorized!')
                setSeverity('error');
            });              
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        const formData = {
            userName: data.get('userName'),
            password: data.get('password')
}
        console.log(formData);
        handleAuth(formData);
    };

    return (
        <ThemeProvider theme={defaultTheme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="userName"
                            label="Email Address"
                            name="userName"
                            autoComplete="email"
                            autoFocus                          
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"                         
                        />
                        <FormControlLabel
                            control={<Checkbox value="remember" color="primary" />}
                            label="Remember me"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            onClick={handleOpen}
                        >
                            Sign In
                        </Button>
                        <Backdrop
                            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
                            open={open}                         
                        >
                            <CircularProgress color="inherit" />
                        </Backdrop>
                        <Grid container>
                            <Grid item xs>
                                <Link href="#" variant="body2">
                                    Forgot password?
                                </Link>
                            </Grid>
                            <Grid item>
                                <Link href="#" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
                <Copyright sx={{ mt: 8, mb: 4 }} />
            </Container>
            {showSnackbar &&
                <Snackbar open={handleSnackbarOpen} autoHideDuration={6000} onClose={handleSnackbarClose}>
                    <Alert onClose={handleSnackbarClose} severity={severity} sx={{ width: '100%' }}>
                        {message}
                    </Alert>
                </Snackbar>
            }
        </ThemeProvider>
    );
}