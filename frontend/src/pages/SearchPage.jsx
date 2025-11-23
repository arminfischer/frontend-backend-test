import { useState, useEffect } from 'react'
import { useNavigate } from 'react-router-dom'
import {
  Box,
  TextField,
  Paper,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Typography,
  CircularProgress,
  InputAdornment,
  Chip
} from '@mui/material'
import SearchIcon from '@mui/icons-material/Search'

function SearchPage() {
  const [query, setQuery] = useState('')
  const [results, setResults] = useState([])
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const navigate = useNavigate()

  useEffect(() => {
    const fetchResults = async () => {
      setLoading(true)
      setError(null)
      
      try {
        const response = await fetch(`/api/search?query=${encodeURIComponent(query)}`)
        if (!response.ok) {
          throw new Error('Failed to fetch search results')
        }
        const data = await response.json()
        setResults(data)
      } catch (err) {
        setError(err.message)
      } finally {
        setLoading(false)
      }
    }

    const timeoutId = setTimeout(() => {
      fetchResults()
    }, 300)

    return () => clearTimeout(timeoutId)
  }, [query])

  const handleDocumentClick = (id) => {
    navigate(`/document/${id}`)
  }

  return (
    <Box>
      <Typography variant="h4" component="h1" gutterBottom>
        Search Documents
      </Typography>
      
      <TextField
        fullWidth
        variant="outlined"
        placeholder="Search for documents..."
        value={query}
        onChange={(e) => setQuery(e.target.value)}
        sx={{ mb: 3 }}
        InputProps={{
          startAdornment: (
            <InputAdornment position="start">
              <SearchIcon />
            </InputAdornment>
          ),
        }}
      />

      {loading && (
        <Box display="flex" justifyContent="center" my={4}>
          <CircularProgress />
        </Box>
      )}

      {error && (
        <Typography color="error" align="center">
          {error}
        </Typography>
      )}

      {!loading && !error && results.length === 0 && query && (
        <Typography align="center" color="text.secondary">
          No results found
        </Typography>
      )}

      {!loading && !error && results.length > 0 && (
        <Paper elevation={2}>
          <List>
            {results.map((result, index) => (
              <ListItem
                key={result.id}
                disablePadding
                divider={index < results.length - 1}
              >
                <ListItemButton onClick={() => handleDocumentClick(result.id)}>
                  <ListItemText
                    primary={
                      <Box display="flex" alignItems="center" gap={1}>
                        <Typography variant="h6">{result.title}</Typography>
                        <Chip
                          label={`Score: ${result.relevance.toFixed(1)}`}
                          size="small"
                          color="primary"
                          variant="outlined"
                        />
                      </Box>
                    }
                    secondary={result.description}
                  />
                </ListItemButton>
              </ListItem>
            ))}
          </List>
        </Paper>
      )}
    </Box>
  )
}

export default SearchPage
