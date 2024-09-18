# Get the current working directory

#current_dir <- getwd()
#relative_path <- "contingency_tables"
#setwd(file.path(current_dir, relative_path))

setwd("C:/Users/asus/Desktop/Final zip/contingency_tables")


csv_files <- list.files(pattern = "\\.csv$")

# Read each CSV file into a data frame
tables <- list()

# Loop through table files and read them into the list
for (i in seq_along(csv_files)) {
    table <- read.csv(csv_files[i])
    tables[[i]] <- table
}

results_df <- data.frame(Table = integer(), P_Value = numeric())
# Perform Fisher's Exact Test on each table
for (i in seq_along(tables)) {
    result <- suppressWarnings(fisher.test(tables[[i]], simulate.p.value = TRUE))
    results_df <- rbind(results_df, data.frame(Table = i, P_Value = result$p.value))
    #cat("Fisher's Exact Test for Table", i, "\n")
    #print(result)
    #cat("\n")
    cat("Table", i, "p-value:", result$p.value, "\n")
}

print(results_df)

#current_dir <- getwd()
#output_file_relative_path <- "EDA_fisher_results.csv"
#output_file_path <- file.path(current_dir, output_file_relative_path)
#write.csv(results_df, file = output_file_path, row.names = FALSE)

write.csv(results_df, file = "c:\\Users\\asus\\Desktop\\Final zip\\EDA_fisher_results.csv", row.names = FALSE)

