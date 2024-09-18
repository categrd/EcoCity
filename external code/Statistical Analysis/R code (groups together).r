# Lista di matrici di contingenza
matrici_contingenza <- list(
  Education = matrix(c(0, 0, 11, 1, 18, 15, 8, 8, 24, 14, 0, 7, 0, 0, 0, 12, 31, 0, 9, 2, 0), nrow = 7, ncol = 3, byrow = FALSE),
  Affective_Symptoms = matrix(c(3, 4, 8, 11, 5, 6, 0, 5, 1, 5, 3, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 
              5, 15, 0, 9, 23, 0, 0, 0, 1, 4, 5, 1, 6, 0, 8, 10, 6, 1, 1, 9, 2), nrow = 16, ncol = 3, byrow = FALSE),
  Rumination = data <- matrix(c(6, 9, 7, 7, 9, 8, 6, 0, 0, 1, 0, 0, 0, 0, 0, 1, 2, 13, 10, 27, 1, 0, 1, 2, 6, 5, 16, 14, 3, 6),
               nrow = 10, ncol = 3, byrow = FALSE),
  Behavioural_Symptoms = data <- matrix(c(5, 8, 1, 7, 7, 7, 10, 7, 1, 0, 0, 0, 0, 0, 0, 0, 1, 3, 0, 8, 5, 36, 2, 0, 0, 0, 4, 11, 6, 17, 3, 5, 6),
               nrow = 11, ncol = 3, byrow = FALSE),
  Anxiety_Personal_Impact = data <- matrix(c(5, 5, 1, 7, 10, 7, 1, 6, 6, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 9, 8, 32, 0, 0, 0, 1, 4, 6, 0, 8, 15, 10, 3, 7),
               nrow = 12, ncol = 3, byrow = FALSE),
  Attribution_Skepticism = matrix(c(6, 12, 12, 14, 8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 18, 11, 9, 14, 0, 0, 0, 0, 0, 14, 8, 14, 1, 17, 0, 0, 0, 0),
               nrow = 14, ncol = 3, byrow = FALSE),
  Impact_Skepticism = matrix(c(10, 14, 0, 7, 12, 10, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 21, 15, 4, 12, 1, 10, 16, 1, 12, 4, 11, 0, 0, 0, 0, 0),
               nrow = 11, ncol = 3, byrow = FALSE),
  Trend_Skepticism = matrix(c(8, 5, 1, 9, 13, 16, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 12, 16, 9, 15, 1, 0, 0, 0, 0, 0, 0, 14, 18, 13, 9, 0, 0, 0, 0, 0),
               nrow = 15, ncol = 3, byrow = FALSE),
  Response_Skepticism = matrix(c(0, 0, 1, 0, 1, 0, 13, 16, 15, 12, 11, 11, 13, 16, 15, 14, 9, 12, 1, 0, 0),
               nrow = 7, ncol = 3, byrow = TRUE),
  Single = matrix(c(25, 28, 53, 0, 49, 5), nrow = 2, ncol = 3, byrow = FALSE),
  Widowed = matrix(c(53, 49, 50, 0, 4, 4), nrow = 2, ncol = 3, byrow = TRUE),
  Separated = matrix(c(53, 44, 54, 0, 9, 0), nrow = 2, ncol = 3, byrow = TRUE)
)

print("Tabella di Contingenza:")
print(matrici_contingenza)

# Funzione per calcolare i risultati del test esatto di Fisher
calcola_risultati_fisher <- function(matrice) {
  return(fisher.test(matrice, simulate.p.value = TRUE))
}

# Lista per salvare i risultati
risultati_lista <- list()

# Ciclo for per il calcolo del test di Fisher per ogni matrice
for (i in seq_along(matrici_contingenza)) {
  risultati_fisher <- calcola_risultati_fisher(matrici_contingenza[[i]])
  risultati_lista[[i]] <- risultati_fisher
}

# Stampa i risultati
for (i in seq_along(risultati_lista)) {
  cat("Risultati del Test Esatto di Fisher per Matrice", i, ":\n")
  print(risultati_lista[[i]])
  cat("\n")
}

# Salva tutti i risultati nella lista
#saveRDS(risultati_lista, "fisher_test_result.rds")

# Save only the p-values as a separate file
p_values_list <- lapply(risultati_lista, function(result) result$p.value)
names(p_values_list) <- names(risultati_lista)  # Assign names to the list elements
#saveRDS(p_values_list, "fisher_test_p_values.rds")
print(p_values_list)
