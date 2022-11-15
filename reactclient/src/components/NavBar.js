import React from 'react';

import {AppBar, Container, IconButton, Toolbar, Box} from "@mui/material"


const NavBar = () => {
    return (
        <AppBar>
            <Container maxWidth='lg'>
                <Toolbar disableGutters>
                    <Box sx={{mr:1}}>
                        <IconButton size='large' color='inherit'>
                           
                        </IconButton>
                    </Box>
                    
               
                </Toolbar>
            </Container>
        </AppBar>
    )
}
export default NavBar