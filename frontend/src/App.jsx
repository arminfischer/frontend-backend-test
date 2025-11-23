import { Routes, Route } from 'react-router-dom'
import { Container, AppBar, Toolbar, Typography, Box } from '@mui/material'
import SearchPage from './pages/SearchPage'
import DocumentDetailPage from './pages/DocumentDetailPage'

function App() {
  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
      <AppBar position="static">
        <Toolbar>
          <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
            Document Search App
          </Typography>
        </Toolbar>
      </AppBar>
      <Container maxWidth="lg" sx={{ mt: 4, mb: 4, flex: 1 }}>
        <Routes>
          <Route path="/" element={<SearchPage />} />
          <Route path="/document/:id" element={<DocumentDetailPage />} />
        </Routes>
      </Container>
    </Box>
  )
}

export default App
